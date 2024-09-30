﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForkliftControlSystem.Domain.Entities
{
    public class Forklift
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string ModelNumber { get; set; }
        public DateTime ManufacturingDate { get; set; }
    }
}
