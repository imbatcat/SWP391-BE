﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// To be finished
namespace PetHealthcare.Server.Models
{
    public class BookingPayment
    {
        [NotMapped]
        public string Prefix { get; } = "BP";

        [Key]
        [Column(TypeName = "char(11)")]
        public string PaymentId { get; set; }

        [DataType(DataType.Currency)]
        public double Price { get; set; }

        [StringLength(50)]
        public string PaymentMethod { get; set; }

        [DataType(DataType.DateTime)]
        public DateOnly PaymentDate { get; set; }

        // Reference entity
        [ForeignKey("AppointmentId")]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public Appointment Appointment { get; set; }
        public string AppointmentId { get; set; }
    }
}
