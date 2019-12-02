using Realms;
using System;
using System.Collections.Generic;
using System.Text;

namespace XNotes.Model
{
    class Note : RealmObject
    {
        [PrimaryKey]
        public String id { get; set; } = Guid.NewGuid().ToString();
        public String title { get; set; }
        public String message { get; set; }
        public List<String> tags { get; }
        public String tagLine { get; set; }


        public Note()
        {
            tagLine = createTagLabel(tags);
        }

        public Note(String title, String message, List<String> tags)
        {
            this.title = title; 
            this.message = message; 
            this.tags =tags;
            this.tagLine = createTagLabel(tags);
        }

        public String createTagLabel(List<String> tags)
        {
            if (tags == null) return "";
            String outputString = "";
            foreach(String tag in tags)
            {
                outputString += $" {tag}";
            }
            Console.WriteLine(outputString);
            return outputString.Trim();
            
        }
    }
}
