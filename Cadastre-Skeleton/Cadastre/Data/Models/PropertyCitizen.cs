﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cadastre.Data.Models
{
    public class PropertyCitizen
    {
        [Required]
        [ForeignKey(nameof(PropertyId))]
        public int PropertyId { get; set; }

        public Property Property { get; set; }

        [Required]
        [ForeignKey(nameof(CitizenId))]
        public int CitizenId { get; set; }

        public Citizen Citizen { get; set;}
    }
}
//•	PropertyId – integer, Primary Key, foreign key (required)
//•	Property – Property
//•	CitizenId – integer, Primary Key, foreign key (required)
//•	Citizen – Citizen