﻿using System;
using Entities.Models;

namespace English.Services.DTOs
{
    public class SubsectionGetDto:BaseEntity
    {

        public string Name { get; set; }

        public int Order { get; set; }

        public Guid? SectionId { get; set; }

    }
}
