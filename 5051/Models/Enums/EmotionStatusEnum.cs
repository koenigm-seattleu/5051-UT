using System.ComponentModel.DataAnnotations;

namespace _5051.Models
{
    /// <summary>
    /// Emotion Status
    /// </summary>
    public enum EmotionStatusEnum
    {
        [Display(Name = "VeryHappy")]
        VeryHappy = 5,

        [Display(Name = "Happy")]
        Happy = 4,

        [Display(Name = "Neutral")]
        Neutral = 3,

        [Display(Name = "Sad")]
        Sad = 2,

        [Display(Name = "VerySad")]
        VerySad = 1,
    }


    public static class Emotion
    {

        // Return the path to the emotion
        public static string GetEmotionURI(EmotionStatusEnum EmotionCurrent)
        {
            var myReturn = "";

            switch (EmotionCurrent)
            {
                case EmotionStatusEnum.VeryHappy:
                    myReturn = "EmotionVeryHappy.png";
                    break;
                case EmotionStatusEnum.Happy:
                    myReturn = "EmotionHappy.png";
                    break;
                case EmotionStatusEnum.Neutral:
                    myReturn = "EmotionNeutral.png";
                    break;
                case EmotionStatusEnum.Sad:
                    myReturn = "EmotionSad.png";
                    break;
                case EmotionStatusEnum.VerySad:
                    myReturn = "EmotionVerySad.png";
                    break;

                default:
                    myReturn = "placeholder.png";
                    break;
            }
            return "/content/img/" + myReturn;
        }
    }
}