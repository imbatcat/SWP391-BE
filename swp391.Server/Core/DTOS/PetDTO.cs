﻿namespace PetHealthcare.Server.Core.DTOS
{
    public class PetDTO
    {
        //public string Prefix { get; }="PE";
        public string ImgUrl { get; set; }
        public string PetName { get; set; }
        public string PetBreed { get; set; }
        public DateOnly PetAge { get; set; }
        public string? Description { get; set; }
        public bool IsMale { get; set; }
        public bool IsCat { get; set; }
        public string? VaccinationHistory { get; set; }
        public bool IsDisable { get; set; }
        public required string AccountId { get; set; }
    }
}
