export default {
    name : "my_reasoning_table",
    props: {
        result_table : {
            type : Array,
            required : true
        },
        my_key : {
            type : String,
            required : true,
        }
    },
    data ()  {
        return {
            icon_data : {
                0 : require('@/assets/img/material_card/ingredient_1.png'),
                1 : require('@/assets/img/material_card/ingredient_2.png'),
                2 : require('@/assets/img/material_card/ingredient_3.png'),
                3 : require('@/assets/img/material_card/ingredient_4.png'),
                4 : require('@/assets/img/material_card/ingredient_5.png'),
                5 : require('@/assets/img/material_card/ingredient_6.png'),
                6 : require('@/assets/img/material_card/ingredient_7.png'),
                7 : require('@/assets/img/material_card/ingredient_8.png'),
            },
            reasoning_data : {
                0 : require('@/assets/img/stamp/rgbl010.png'),
                1 : require('@/assets/img/stamp/rgbl101.png'),
                2 : require('@/assets/img/stamp/rglb100.png'),
                3 : require('@/assets/img/stamp/rglb011.png'),
                4 : require('@/assets/img/stamp/rlgb001.png'),
                5 : require('@/assets/img/stamp/rlgb110.png'),
                6 : require('@/assets/img/stamp/rlglbl000.png'),
                7 : require('@/assets/img/stamp/rlglbl111.png'),
            },
            picked : -1,
            reasoning_table_onoff : false,
        }
    },

    methods : {
        click_reasoning_ele : function(index , key) {
            let data = {
                x : index,
                y : key,
                change_val : this.picked,
            }
            this.$emit('click_reasoning_table' , data);
            this.picked = -1;
            this.reasoning_table_onoff = false;
        },
        open_reasoning_table_modal : function( index, key ) {
            this.$refs.index_val.innerText = index;
            this.$refs.key_val.innerText = key;
            //console.log(this.$refs.index_val.innerText);
            //console.log(index + " " + key);
            this.reasoning_table_onoff = true;
        },
        reset_val : function () {
            this.picked = -1;
        }

        },        
    }