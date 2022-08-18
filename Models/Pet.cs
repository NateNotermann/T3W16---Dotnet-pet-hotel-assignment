using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace pet_hotel
{
    public enum PetBreedType
    {
        Shepherd,
        Poodle,
        Beagle,
        Bulldog,
        Terrier,
        Boxer,
        Labrador,
        Retriever,
        Cat,
        Bunny

    }
    public enum PetColor
    {
        White,
        Black,
        Golden,
        Tricolor,
        Spotted,
        Gradient,
        BootedChocolateWhite
    }
    public class Pet
    {
        public int id { get; set; }
        public string name { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PetBreedType breed { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PetColor color { get; set; }
            
        public Nullable<DateTime> checkedInAt { get; set; }
        
        [ForeignKey("PetOwner")]        
        public int PetOwnerId { get; set; }

        public PetOwner petOwner { get; set; }
        
        }

}
