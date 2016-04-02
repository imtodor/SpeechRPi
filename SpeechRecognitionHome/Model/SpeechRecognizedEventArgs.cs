using System;

namespace SpeechRPi.Model
{
    public class SpeechRecognizedEventArgs : EventArgs
    {
        public string RecognizedText { get; private set; }

        public SpeechRecognizedEventArgs(string recognizedText)
        {
            if (string.IsNullOrEmpty(recognizedText))
            {
                throw new ArgumentNullException("recognizedText", "Recognized text cannot be null or empty string.");
            }

            this.RecognizedText = recognizedText;
        }
    }
}