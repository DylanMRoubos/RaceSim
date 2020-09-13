﻿using System;
using System.Collections.Generic;

namespace Model
{
    public class Track
    {
        public string Name { get; set; }
        public LinkedList<Section> Sections { get; set; }

        public Track(string name, SectionTypes[] sections)
        {
            Name = name;
            Sections = new LinkedList<Section>();

            foreach (SectionTypes section in sections) {
                Sections.AddFirst(new Section(section));
            }
        }
    }
}
