﻿using System;
namespace Model
{
    public class Section
    {
        public SectionTypes SectionType { get; set; }

        public Section(SectionTypes SectionType)
        {
            this.SectionType = SectionType;
        }

    }
}
