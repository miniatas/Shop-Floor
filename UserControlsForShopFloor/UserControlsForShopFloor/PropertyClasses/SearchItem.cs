using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopFloor.Classes
{
    public class SearchItem
    {
       
            public string _Name;
            public int _Id;
   
      public SearchItem(string name, int id)
            {
                _Name = name;
                _Id = id;
            }

            public string Name
            {
                get { return _Name; }
                set { _Name = value; }
            }

            public int Id
            {
                get { return _Id; }
                set { _Id = value; }
            }


        public override string ToString()
        {

            return Name;
        }
    }


}
