using System;
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
            Sections = ConvertSectionTypesToLinkedList(sections);

        }
        public LinkedList<Section> ConvertSectionTypesToLinkedList(SectionTypes[] sections)
        {
            LinkedList<Section> Sections = new LinkedList<Section>();

            foreach (SectionTypes section in sections)
            {
                Sections.AddLast(new Section(section));
            }

            return Sections;
        }
    }
}
