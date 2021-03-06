﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Model
{
    public class Pet
    {
        public int PetId { get; set; }
        public int PetOwnerId { get; set; }
        [Required]
        [Display(Name = "Pet Name")]
        public string PetName { get; set; }
        [Required]
        [Display(Name = "Pet Type")]
        public PetType PetType { get; set; }
        [Required]
        [Display(Name = "Date Of Birth")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime DateOfBirth { get; set; }

        internal static DataTable ToDataTable(IList<Pet> pets)
        {
            DataTable table = CreateTable();
            foreach (var pet in pets)
            {
                table.Rows.Add(pet.PetName, pet.DateOfBirth.ToString(), pet.PetType.ToString());
            }

            return table;
        }

        private static DataTable CreateTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Item1", typeof(string));
            table.Columns.Add("Item2", typeof(string));
            table.Columns.Add("Item3", typeof(string));
            return table;
        }
    }
}
