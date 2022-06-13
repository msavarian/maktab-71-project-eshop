﻿using System;
using System.Collections.Generic;

namespace App.Domain.Core.ProductAggrigate.Entites
{
    public class Collection
    {
        public Collection()
        {
            CollectionProducts = new HashSet<CollectionProduct>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime CreationDate { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<CollectionProduct> CollectionProducts { get; set; }
    }
}