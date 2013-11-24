using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClearPeople.Sitecore
{
    public class OriginalMultilistField : DelimitedField
    {
        public ID[] TargetIDs
        {
            get
            {
                ArrayList arrayLists = new ArrayList();
                string value = base.Value;
                string[] strArrays = value.Split(new char[] { '|' });
                for (int i = 0; i < (int)strArrays.Length; i++)
                {
                    string str = strArrays[i];
                    if (str.Length > 0 && ID.IsID(str))
                    {
                        arrayLists.Add(ID.Parse(str));
                    }
                }
                return arrayLists.ToArray(typeof(ID)) as ID[];
            }
        }

        public OriginalMultilistField(Field innerField)
            : base(innerField, '|')
        {
        }

        public Item[] GetItems()
        {
            ArrayList arrayLists = new ArrayList();
            Database database = this.GetDatabase();
            if (database == null)
            {
                return null;
            }
            ID[] targetIDs = this.TargetIDs;
            for (int i = 0; i < (int)targetIDs.Length; i++)
            {
                Item item = database.GetItem(targetIDs[i]);
                if (item != null)
                {
                    arrayLists.Add(item);
                }
            }
            return arrayLists.ToArray(typeof(Item)) as Item[];
        }

        public static implicit operator OriginalMultilistField(Field field)
        {
            if (field == null)
            {
                return null;
            }
            return new OriginalMultilistField(field);
        }
    }
}
