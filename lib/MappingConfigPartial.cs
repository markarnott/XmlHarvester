﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LantanaGroup.XmlDocumentConverter
{
    public partial class MappingConfig
    {
        private static void AssociateParents(IEnumerable<MappingGroup> groups, MappingGroup parent = null)
        {
            foreach (MappingGroup next in groups)
            {
                next.Parent = parent;

                AssociateParents(next.Group, next);
            }
        }
        public static MappingConfig LoadFromFileWithParents(string fileName)
        {
            try
            {
                MappingConfig obj = LoadFromFile(fileName);
                AssociateParents(obj.Group);
                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception("Error loading config file: " + ex.Message, ex);
            }
        }

        public static string GetOutputFileNameWithoutExtension()
        {
            return DateTime.Now.ToString("yyyy-MM-dd hhmm tt");
        }
    }
}
