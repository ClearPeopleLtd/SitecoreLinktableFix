using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Links;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClearPeople.Sitecore.Data.Fields
{
    public class MultilistField: global::Sitecore.Data.Fields.MultilistField
    {
        public MultilistField(Field innerField) : base(innerField)
        {
        }

        public override void ValidateLinks(LinksValidationResult result)
        {
            Database database = this.GetDatabase();
            if (database != null)
            {
                string[] items = base.Items;
                for (int i = 0; i < (int)items.Length; i++)
                {
                    string str = items[i];
                    if (ID.IsID(str))
                    {
                        ID d = ID.Parse(str);
                        if (!ItemUtil.IsNull(d) && !d.IsNull)
                        {
                            Item item = database.GetItem(d);
                            if (item == null)
                            {
                                result.AddBrokenLink(str);
                            }
                            else
                            {
                                result.AddValidLink(item, item.Paths.FullPath);
                            }
                        }
                    }
                }

            }
        }
    }
}
