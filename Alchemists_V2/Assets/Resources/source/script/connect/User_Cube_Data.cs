using System.Collections.Generic;

namespace Alchemists_data
{
    // 행동 큐브 데이터
    public class User_Cube_Data
    {
        /*
          user_key : data.user_key,
          user_color : user_color,
          order : '',
          length : 3,
          cube { 1 : Cube_On_Board, 2, 3}
         * */
        public string user_key { get; }
        public string user_color { get; }
        public int order { get; }
        public int length { get; }
        public Dictionary<int, Cube_On_Board> cube { get; set; }

        public User_Cube_Data (string user_key, string user_color, int order, int length, Dictionary<int, Cube_On_Board> cube)
        {
            this.user_color = user_color;
            this.user_key = user_key;
            this.order = order;
            this.length = length;
            this.cube = cube;
        }
    }
    // cube 클래스
    public class Cube_On_Board
    {
        /*
         *    num : 1,
              cnt : 1,
              is_select : false,
         */
        public int num { get; }
        public int cnt { get; }
        public bool is_select { get; set; }
        public Cube_On_Board(int num, int cnt, bool is_select)
        {
            this.num = num;
            this.cnt = cnt;
            this.is_select = is_select;
        }
    }
    // user_cube_data[board_num][user_num].cube[cube_num].is_select
}