using System.ComponentModel.DataAnnotations;

namespace api.Enums
{
    public enum CharacterRole
    {
        [Display(Name = "Hero")] Hero = 1,
        [Display(Name = "Villain")] Villain = 2,
        [Display(Name = "Prince")] Prince = 3,
        [Display(Name = "Princess")] Princess = 4,
        [Display(Name = "NPC")] NPC = 3,
    }
}