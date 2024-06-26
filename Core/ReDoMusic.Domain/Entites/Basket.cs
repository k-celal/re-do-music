﻿using ReDoMusic.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReDoMusic.Domain.Entites
{
    public class Basket : EntityBase<Guid>
    {
        public List<BasketItem> Items { get; set; }
    }

    public class BasketItem: EntityBase<Guid>
    {
        public Instrument Instrument { get; set; }
        public int Quantity { get; set; } 
    }


}

