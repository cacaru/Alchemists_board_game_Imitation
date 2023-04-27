using System.Collections;
using UnityEngine;

namespace Alchemists_data
{
    public class Selling_Potion_End
    {
        /*
        selling_success : selling_success,
      potion : data.what_kind_sell_potion,
      user_key : data.user_key,
      user_color : data.user_color,
        */
        public bool selling_success { get; set; }
        public string what_kind_sell_potion { get; set; }
        public string user_key { get; set; }
        public string user_color { get; set; }

        public Selling_Potion_End(bool selling_success, string what_kind_sell_potion, string user_key, string user_color)
        {
            this.selling_success = selling_success;
            this.what_kind_sell_potion = what_kind_sell_potion;
            this.user_color = user_color;
            this.user_key = user_key;
        }

        public string print()
        {
            return "Selling_Potion_End >> selling_success : " + this.selling_success.ToString() +
                                        " / what_kind_sell_potion : " + this.what_kind_sell_potion +
                                        " / user_key : " + this.user_key +
                                        " / user_color : " + this.user_color;
        }
    }
}