using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpeechLib;

namespace Qlasha
{
    class Mouth
    {
        private SpVoice SpeakerSAPI5;
        private Memory MyMemory;
        private String[] SystemVoicesDescription;
        private String[] SystemVoicesNames;
        private Int32 SystemVoicesCount;

        public Mouth(Memory MyMemory)
        {
            Init(MyMemory);
            this.SetRate(Properties.Resources.CfgSpeekRate);
            this.SetVolume(Properties.Resources.CfgSpeekVolume);
        }

        public void Stop()
        {
            SpeakerSAPI5.Speak(null, (SpeechVoiceSpeakFlags)2);
            SpeakerSAPI5.Resume();
        }

        private void Init(Memory MyMemory)
        {
            this.MyMemory = MyMemory;

            SpeakerSAPI5 = new SpVoice();
            SpeakerSAPI5.EndStream += new _ISpeechVoiceEvents_EndStreamEventHandler(SpeakerSAPI5_SpeakCompleted);

            Int32 i = 0;
            String Country;
            SystemVoicesCount = SpeakerSAPI5.GetVoices("", "").Count;
            SystemVoicesDescription = new String[SystemVoicesCount];
            SystemVoicesNames = new String[SystemVoicesCount];

            // Получить все звуковые схемы
            foreach (ISpeechObjectToken SysVoice in SpeakerSAPI5.GetVoices("", ""))
            {
                try
                {
                    Country = System.Globalization.CultureInfo.GetCultureInfo(Convert.ToInt32(SysVoice.GetAttribute("Language").Substring(0,3), 16)).ThreeLetterWindowsLanguageName;
                }
                catch
                {
                    Country = "???";
                }
                
                SystemVoicesDescription[i] = Country + '[' + SysVoice.GetAttribute("Gender") + "] " + SysVoice.GetAttribute("Name");
                SystemVoicesNames[i++] = SysVoice.GetAttribute("Name");
            }


            String[] VoiceSelectAlg = Properties.Resources.CfgVoiceSelectAlg.Split(new String[] { Properties.Resources.CfgSeparator }, System.StringSplitOptions.RemoveEmptyEntries);
            for (Int32 n = 0; n < VoiceSelectAlg.Length; n++)
            {
                for (i = 0; i < SystemVoicesCount; i++)
                {
                    if (SystemVoicesDescription[i].Contains(VoiceSelectAlg[n]))
                    {
                        try { SpeakerSAPI5.Voice = SpeakerSAPI5.GetVoices(String.Format("Name={0}", SystemVoicesNames[i]), "").Item(0); }
                        catch (Exception e)
                        {
                            MyMemory.ToRemember(String.Format(Properties.Resources.StrCantChangeVoice, SystemVoicesNames[i], e.ToString()), 0);
                            continue;
                        }
                        goto END_VOICE_SELECTION;
                    }
                }
            }
            END_VOICE_SELECTION:
            return;
        }
        
        void SpeakerSAPI5_SpeakCompleted(int StreamNumber, object StreamPosition)
        {
            String Message = MyMemory.Remember();
            if (Message == Properties.Resources.StrEmpty) return;
            SpeakerSAPI5.Speak(Message, SpeechVoiceSpeakFlags.SVSFlagsAsync);
        }

        public Int32 GetVolume
        {
            get { return SpeakerSAPI5.Volume; }
        }

        public void SetVolume(Int32 Volume)
        {
            if (Volume < 0 || Volume > 100) return;
            SpeakerSAPI5.Volume = Volume;
        }

        public void SetVolume(String SVolume)
        {
            Int32 Volume;
            try { Volume = Convert.ToInt32(SVolume); }
            catch { return; }
            SetVolume(Volume);
        }

        public Int32 GetRate
        {
            get { return SpeakerSAPI5.Rate; }
        }

        public void SetRate(Int32 Rate)
        {
            if (Rate < -10 || Rate > 10) return;
            SpeakerSAPI5.Rate = Rate;
        }

        public void SetRate(String SRate)
        {
            Int32 Rate;
            try{ Rate = Convert.ToInt32(SRate); }
            catch { return; }
            SetRate(Rate);
        }

        public bool SetVoice(String Voice)
        {
            try
            {
                SpeakerSAPI5.Voice = SpeakerSAPI5.GetVoices(string.Format("Name={0}", Voice), "").Item(0);
                return true;
            }
            catch (Exception e)
            {
                MyMemory.ToRemember(String.Format(Properties.Resources.StrCantChangeVoice, Voice, e.ToString()), 0);
            }
            return false;
        }

        public String GetVoice
        {
            get { return SpeakerSAPI5.Voice.GetAttribute("Name"); }
        }

        public String[] GetInstalledVoicesNames
        {
            get { return SystemVoicesNames; }
        }

        public String[] GetInstalledVoicesDescription
        {
            get { return SystemVoicesDescription; }
        }

        public Int32 GetInstalledVoicesCount
        {
            get { return SystemVoicesCount; }
        }

        public void Speak()
        {
            System.Threading.Thread.Sleep(50);
            if (!SpeakerSAPI5.WaitUntilDone(0)) return;

            String Message = MyMemory.Remember();
            if (Message == Properties.Resources.StrEmpty) return;

            SpeakerSAPI5.Speak(Message, SpeechVoiceSpeakFlags.SVSFlagsAsync);
        }
    }
}
