<template>
    <div>
        <!-- 용병에게 물약 판매 모달 -->
        <b-modal id="sell_potion_to_adv_modal_onoff" 
            centered 
            lazy
            size="lg"
            ref="sell_potion_to_adv_modal_onoff"
            v-model="sell_potion_to_adv_modal_onoff"
            header-text-variant="light"
            header-bg-variant="success"
            hide-footer
            hide-header-close
            hide-backdrop
            no-close-on-backdrop
            title="판매 가능 목록"
            >
            <b-button pill class="sell_potion_reasoning_table_btn" id="my_reasoning_table_open" v-on:click="reasoning_table_open">
                추리 테이블 보기
            </b-button>
            <b-container v-if="this.sell_potion_turn == true">
                <b-row>
                    <b-col cols="5" v-if="this.round_cont == 2">
                        <img class="sell_part_icon"      v-if="this.random_adv_list[0] == 1" src="@/assets/img/adventurer/adventurer_1.png">
                        <img class="sell_part_icon" v-else-if="this.random_adv_list[0] == 2" src="@/assets/img/adventurer/adventurer_2.png">
                        <img class="sell_part_icon" v-else-if="this.random_adv_list[0] == 3" src="@/assets/img/adventurer/adventurer_3.png">
                        <img class="sell_part_icon" v-else-if="this.random_adv_list[0] == 4" src="@/assets/img/adventurer/adventurer_4.png">
                        <img class="sell_part_icon" v-else-if="this.random_adv_list[0] == 5" src="@/assets/img/adventurer/adventurer_5.png">
                        <img class="sell_part_icon" v-else-if="this.random_adv_list[0] == 6" src="@/assets/img/adventurer/adventurer_6.png">
                    </b-col>
                    <b-col cols="5" v-else-if="this.round_cont == 3" >
                        <img class="sell_part_icon"      v-if="this.random_adv_list[1] == 1" src="@/assets/img/adventurer/adventurer_1.png">
                        <img class="sell_part_icon" v-else-if="this.random_adv_list[1] == 2" src="@/assets/img/adventurer/adventurer_2.png">
                        <img class="sell_part_icon" v-else-if="this.random_adv_list[1] == 3" src="@/assets/img/adventurer/adventurer_3.png">
                        <img class="sell_part_icon" v-else-if="this.random_adv_list[1] == 4" src="@/assets/img/adventurer/adventurer_4.png">
                        <img class="sell_part_icon" v-else-if="this.random_adv_list[1] == 5" src="@/assets/img/adventurer/adventurer_5.png">
                        <img class="sell_part_icon" v-else-if="this.random_adv_list[1] == 6" src="@/assets/img/adventurer/adventurer_6.png">
                    </b-col>
                    <b-col cols="5" v-else-if="this.round_cont == 4" >
                        <img class="sell_part_icon"      v-if="this.random_adv_list[2] == 1" src="@/assets/img/adventurer/adventurer_1.png">
                        <img class="sell_part_icon" v-else-if="this.random_adv_list[2] == 2" src="@/assets/img/adventurer/adventurer_2.png">
                        <img class="sell_part_icon" v-else-if="this.random_adv_list[2] == 3" src="@/assets/img/adventurer/adventurer_3.png">
                        <img class="sell_part_icon" v-else-if="this.random_adv_list[2] == 4" src="@/assets/img/adventurer/adventurer_4.png">
                        <img class="sell_part_icon" v-else-if="this.random_adv_list[2] == 5" src="@/assets/img/adventurer/adventurer_5.png">
                        <img class="sell_part_icon" v-else-if="this.random_adv_list[2] == 6" src="@/assets/img/adventurer/adventurer_6.png">
                    </b-col>
                    <b-col cols="5" v-else-if="this.round_cont == 5" >
                        <img class="sell_part_icon"      v-if="this.random_adv_list[3] == 1" src="@/assets/img/adventurer/adventurer_1.png">
                        <img class="sell_part_icon" v-else-if="this.random_adv_list[3] == 2" src="@/assets/img/adventurer/adventurer_2.png">
                        <img class="sell_part_icon" v-else-if="this.random_adv_list[3] == 3" src="@/assets/img/adventurer/adventurer_3.png">
                        <img class="sell_part_icon" v-else-if="this.random_adv_list[3] == 4" src="@/assets/img/adventurer/adventurer_4.png">
                        <img class="sell_part_icon" v-else-if="this.random_adv_list[3] == 5" src="@/assets/img/adventurer/adventurer_5.png">
                        <img class="sell_part_icon" v-else-if="this.random_adv_list[3] == 6" src="@/assets/img/adventurer/adventurer_6.png">
                    </b-col>
                    <b-col cols="5" v-else-if="this.round_cont == 6" >
                        <img class="sell_part_icon"      v-if="this.random_adv_list[4] == 1" src="@/assets/img/adventurer/adventurer_1.png">
                        <img class="sell_part_icon" v-else-if="this.random_adv_list[4] == 2" src="@/assets/img/adventurer/adventurer_2.png">
                        <img class="sell_part_icon" v-else-if="this.random_adv_list[4] == 3" src="@/assets/img/adventurer/adventurer_3.png">
                        <img class="sell_part_icon" v-else-if="this.random_adv_list[4] == 4" src="@/assets/img/adventurer/adventurer_4.png">
                        <img class="sell_part_icon" v-else-if="this.random_adv_list[4] == 5" src="@/assets/img/adventurer/adventurer_5.png">
                        <img class="sell_part_icon" v-else-if="this.random_adv_list[4] == 6" src="@/assets/img/adventurer/adventurer_6.png">
                    </b-col>

                    <b-col>
                        <!-- 자신의 할인 카드 제시 // 클릭하면 선택되어 보이게 하기 -->
                        <b-row v-if="this.is_end_discount == false && this.im_end_discount == false">
                            <b-col >
                                <b-row v-if="this.my_data.user_key == this.my_key" style="height: 200px;">
                                    <b-col v-for="(data, name) in this.my_data.user_ingame_data.discount_adventurer" :key="name">
                                        <img class="v_coin_icon" ref="ad_0_ico" v-if="data == true && name == 'ad_0'"      :src="adv_icon_data[0]" @click="select_adv_dis_coin(1)" >
                                        <img class="v_coin_icon" ref="ad_1_ico" v-else-if="data == true && name == 'ad_1'" :src="adv_icon_data[1]" @click="select_adv_dis_coin(2)" >
                                        <img class="v_coin_icon" ref="ad_2_ico" v-else-if="data == true && name == 'ad_2'" :src="adv_icon_data[2]" @click="select_adv_dis_coin(3)" >
                                        <img class="v_coin_icon" ref="ad_3_ico" v-else-if="data == true && name == 'ad_3'" :src="adv_icon_data[3]" @click="select_adv_dis_coin(4)" >
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col>
                                        <!-- 제시하기 버튼 -->
                                        <b-button variant="outline-success" class="adv_discount_suggest_btn" @click="adv_dis_confirm">
                                            제시하기
                                        </b-button>
                                    </b-col>
                                </b-row>
                            </b-col>
                        </b-row>
                            <!-- 다른 사람이 선택중임을 알려야함 -->
                        <b-row v-else-if="this.is_end_discount == false && this.im_end_discount == true">
                            <b-col>
                                <span>
                                    다른 사람이 선택 중 입니다. <br>
                                    잠시 기다려주세요
                                </span>
                            </b-col>
                        </b-row>

                        <b-row v-else-if="this.is_end_discount == true && this.im_end_discount == true">
                            <!-- 코인 끝난 이후 순서 칸  // 제시된 카드들을 서버에 전송 후 다시 넘겨받아 리스트를 순서대로 출력-->
                            <b-col>
                                <b-row>
                                    <b-col v-for="data in this.discount_coin_list" :key="data">
                                        <img class="v_coin_icon" :class="data.color" v-if="data.dis_coin_num == '0'"      src="@/assets/img/adventurer/ad_0.png" />
                                        <img class="v_coin_icon" :class="data.color" v-else-if="data.dis_coin_num == '1'" src="@/assets/img/adventurer/ad_1.png" />
                                        <img class="v_coin_icon" :class="data.color" v-else-if="data.dis_coin_num == '2'" src="@/assets/img/adventurer/ad_2.png" />
                                        <img class="v_coin_icon" :class="data.color" v-else-if="data.dis_coin_num == '3'" src="@/assets/img/adventurer/ad_3.png" />
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col>
                                        <span>
                                            물약을 판매하고 받을 골드를 선택합니다.<br>
                                            제시한 할인가와 합쳤을 때 0보다 낮은 금액은 선택할 수 없습니다.
                                        </span>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col class="mt_10">
                                        <img class="adv_sell_potion_icon" id="adv_sell_4" ref="sell_po_4" src="@/assets/img/adventurer/sell_4.png" @click="sell_potion_set_price(4)" />    
                                        <b-popover
                                            target="adv_sell_4"
                                            placement="bottom"
                                            title="판매 설명"
                                            triggers="hover"
                                            content="판매할 물약과 정확히 일치한 물약을 판매해야 골드를 획득합니다."
                                            ></b-popover>
                                    </b-col>
                                    <b-col class="mt_10">
                                        <img class="adv_sell_potion_icon" id="adv_sell_3" ref="sell_po_3" src="@/assets/img/adventurer/sell_3.png" @click="sell_potion_set_price(3)" />
                                        <b-popover
                                            target="adv_sell_3"
                                            placement="bottom"
                                            title="판매 설명"
                                            triggers="hover"
                                            content="판매할 물약과 색이 다르지만 부호가 일치한 물약을 판매해야 골드를 획득합니다."
                                            ></b-popover>
                                    </b-col>
                                    <b-col>
                                        <img class="adv_sell_potion_icon" id="adv_sell_2" ref="sell_po_2" src="@/assets/img/adventurer/sell_2.png" @click="sell_potion_set_price(2)" />
                                        <b-popover
                                            target="adv_sell_2"
                                            placement="bottom"
                                            title="판매 설명"
                                            triggers="hover"
                                            content="판매할 물약과 색이 다르지만 부호가 중성을 포함하여 일치한 물약을 판매해야 골드 획득이 가능합니다. 단, 명성점수 1점을 잃습니다."
                                            ></b-popover>
                                    </b-col>
                                    <b-col class="mt_10">
                                        <img class="adv_sell_potion_icon" id="adv_sell_1" ref="sell_po_1" src="@/assets/img/adventurer/sell_1.png" @click="sell_potion_set_price(1)" />
                                        <b-popover
                                            target="adv_sell_1"
                                            placement="bottom"
                                            title="판매 설명"
                                            triggers="hover"
                                            content="판매할 물약과 전혀 관계없어도 골드 획득이 가능합니다. 단, 명성점수 1점을 잃습니다."
                                            ></b-popover>
                                    </b-col>
                                </b-row>
                                <b-row class="mt_10">
                                    <b-col>
                                        <span class="adv_sell_explain">
                                            4골드 판매
                                        </span>
                                    </b-col>
                                    <b-col>
                                        <span class="adv_sell_explain">
                                            3골드 판매
                                        </span>
                                    </b-col>
                                    <b-col>
                                        <span class="adv_sell_explain">
                                            2골드 판매
                                        </span>
                                    </b-col>
                                    <b-col>
                                        <span class="adv_sell_explain">
                                            1골드 판매
                                        </span>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col>
                                        <span class="now_selling_price" v-if="this.final_selling_price > 0 "> {{ this.final_selling_price }} </span> <span> 원</span>
                                    </b-col>
                                    <b-col>
                                        <b-button variant="outline-success" class="adv_sell_potion_btn" v-on:click="decision_sell_potion_price">
                                            판매가 결정하기
                                        </b-button>
                                    </b-col>
                                </b-row>
                            </b-col>
                        </b-row>
                    </b-col>
                </b-row>
            </b-container>

            <b-container v-else>
                <b-row>
                    <b-col>
                        <span class="not_sell_potion_phase">
                            다른 유저가 이번 보드를 진행중입니다.<br>
                            이번 보드는 차례가 길어 대기 시간이 길어질 수 있습니다.
                        </span>
                    </b-col>
                </b-row>
            </b-container>
        </b-modal>

        <!-- 판매용 물약 생성 모달 -->
        <b-modal id="make_potion_to_sell_onoff" 
            size="xl"
            ref="make_potion_to_sell_onoff"
            v-model="make_potion_to_sell_onoff"
            header-text-variant="light"
            header-bg-variant="success"
            hide-footer
            hide-header-close
            hide-backdrop
            no-close-on-backdrop
            title="물약 생성하기"
            >
            <div>
                <div class="select_potion_modal">
                    <p>어떤 물약을 생성하시겠습니까?</p>
                    <div class="make_potion_what">
                        <b-container>
                            <b-row v-if="this.merchant_use == false">
                                <b-col v-for="(is, name) in this.adventurer_card_data[this.random_adv_list[this.round_cont - 2]]" :key="is">
                                    <img class="make_potion_kind_icon" v-if="name == 'red_1' && is == true " src="@/assets/img/game_icon/red_+.png" @click="select_sell_potion_kind('red_1')" />
                                    <img class="make_potion_kind_icon" v-else-if="name == 'red_0' && is == true " src="@/assets/img/game_icon/red_-.png" @click="select_sell_potion_kind('red_0')" />
                                    <img class="make_potion_kind_icon" v-else-if="name == 'green_1' && is == true " src="@/assets/img/game_icon/green_+.png" @click="select_sell_potion_kind('green_1')" />
                                    <img class="make_potion_kind_icon" v-else-if="name == 'green_0' && is == true " src="@/assets/img/game_icon/green_-.png" @click="select_sell_potion_kind('green_0')" />
                                    <img class="make_potion_kind_icon" v-else-if="name == 'blue_1' && is == true " src="@/assets/img/game_icon/blue_+.png" @click="select_sell_potion_kind('blue_1')" />
                                    <img class="make_potion_kind_icon" v-else-if="name == 'blue_0' && is == true " src="@/assets/img/game_icon/blue_-.png" @click="select_sell_potion_kind('blue_0')" />
                                </b-col>

                                <b-col cols="5">
                                    <p class="make_potion_kind_select">
                                        현재 선택한 물약
                                    </p>
                                    <img class="select_potion_kind_icon" v-if="this.what_kind_sell_potion == 'red_1'" src="@/assets/img/game_icon/red_+.png" />
                                    <img class="select_potion_kind_icon" v-else-if="this.what_kind_sell_potion == 'red_0'" src="@/assets/img/game_icon/red_-.png" />
                                    <img class="select_potion_kind_icon" v-else-if="this.what_kind_sell_potion == 'green_1'" src="@/assets/img/game_icon/green_+.png" />
                                    <img class="select_potion_kind_icon" v-else-if="this.what_kind_sell_potion == 'green_0'" src="@/assets/img/game_icon/green_-.png" />
                                    <img class="select_potion_kind_icon" v-else-if="this.what_kind_sell_potion == 'blue_1'" src="@/assets/img/game_icon/blue_+.png" />
                                    <img class="select_potion_kind_icon" v-else-if="this.what_kind_sell_potion == 'blue_0'" src="@/assets/img/game_icon/blue_-.png" />
                                </b-col>
                            </b-row>
                            <b-row v-if="this.merchant_use == true">
                                <b-col v-for="(is, name) in this.adventurer_card_ori_data[this.random_adv_list[this.round_cont - 2]]" :key="is">
                                    <img class="make_potion_kind_icon" v-if="name == 'red_1' && is == true " src="@/assets/img/game_icon/red_+.png" @click="select_sell_potion_kind('red_1')" />
                                    <img class="make_potion_kind_icon" v-else-if="name == 'red_0' && is == true " src="@/assets/img/game_icon/red_-.png" @click="select_sell_potion_kind('red_0')" />
                                    <img class="make_potion_kind_icon" v-else-if="name == 'green_1' && is == true " src="@/assets/img/game_icon/green_+.png" @click="select_sell_potion_kind('green_1')" />
                                    <img class="make_potion_kind_icon" v-else-if="name == 'green_0' && is == true " src="@/assets/img/game_icon/green_-.png" @click="select_sell_potion_kind('green_0')" />
                                    <img class="make_potion_kind_icon" v-else-if="name == 'blue_1' && is == true " src="@/assets/img/game_icon/blue_+.png" @click="select_sell_potion_kind('blue_1')" />
                                    <img class="make_potion_kind_icon" v-else-if="name == 'blue_0' && is == true " src="@/assets/img/game_icon/blue_-.png" @click="select_sell_potion_kind('blue_0')" />
                                </b-col>

                                <b-col cols="5">
                                    <p class="make_potion_kind_select">
                                        현재 선택한 물약
                                    </p>
                                    <img class="select_potion_kind_icon" v-if="this.what_kind_sell_potion == 'red_1'" src="@/assets/img/game_icon/red_+.png" />
                                    <img class="select_potion_kind_icon" v-else-if="this.what_kind_sell_potion == 'red_0'" src="@/assets/img/game_icon/red_-.png" />
                                    <img class="select_potion_kind_icon" v-else-if="this.what_kind_sell_potion == 'green_1'" src="@/assets/img/game_icon/green_+.png" />
                                    <img class="select_potion_kind_icon" v-else-if="this.what_kind_sell_potion == 'green_0'" src="@/assets/img/game_icon/green_-.png" />
                                    <img class="select_potion_kind_icon" v-else-if="this.what_kind_sell_potion == 'blue_1'" src="@/assets/img/game_icon/blue_+.png" />
                                    <img class="select_potion_kind_icon" v-else-if="this.what_kind_sell_potion == 'blue_0'" src="@/assets/img/game_icon/blue_-.png" />
                                </b-col>
                            </b-row>
                        </b-container>
                    </div>
                    <div class="make_potion_ingre_select_area">
                        <p>
                            선택할 수 있는 재료카드 
                        </p>
                        <b-container>
                            <b-row v-if="this.my_data.user_key == this.my_key"> 
                                <b-col v-for="( value , name ) in this.my_data.user_ingame_data.ingredient" :key="value">
                                    <span class="card_and_count" v-if="name == 'card_1' && value > 0" >
                                        <img class="sell_card_img" :class="{ 'test_border' : ingre_test_boarder[1]}" src="@/assets/img/material_card/card_1.png" @click="click_test_ingredient_check(name)" />
                                        <span> {{ value }} 장</span>
                                    </span>
                                    <span class="card_and_count" v-else-if="name == 'card_2' && value > 0" >
                                        <img class="sell_card_img" :class="{ 'test_border' : ingre_test_boarder[2]}" src="@/assets/img/material_card/card_2.png" @click="click_test_ingredient_check(name)" />
                                        <span>{{ value }} 장</span>
                                    </span>
                                    <span class="card_and_count" v-else-if="name == 'card_3' && value > 0" >
                                        <img class="sell_card_img" :class="{ 'test_border' : ingre_test_boarder[3]}" src="@/assets/img/material_card/card_3.png" @click="click_test_ingredient_check(name)" />
                                        <span>{{ value }} 장</span>
                                    </span>
                                    <span class="card_and_count" v-else-if="name == 'card_4' && value > 0" >
                                        <img class="sell_card_img" :class="{ 'test_border' : ingre_test_boarder[4]}" src="@/assets/img/material_card/card_4.png" @click="click_test_ingredient_check(name)" />
                                        <span>{{ value }} 장</span>
                                    </span>
                                    <span class="card_and_count" v-else-if="name == 'card_5' && value > 0" >
                                        <img class="sell_card_img" :class="{ 'test_border' : ingre_test_boarder[5]}" src="@/assets/img/material_card/card_5.png" @click="click_test_ingredient_check(name)"  />
                                        <span>{{ value }} 장</span>
                                    </span>
                                    <span class="card_and_count" v-else-if="name == 'card_6' && value > 0" >
                                        <img class="sell_card_img" :class="{ 'test_border' : ingre_test_boarder[6]}" src="@/assets/img/material_card/card_6.png" @click="click_test_ingredient_check(name)" />
                                        <span>{{ value }} 장</span>
                                    </span>
                                    <span class="card_and_count" v-else-if="name == 'card_7' && value > 0" >
                                        <img class="sell_card_img" :class="{ 'test_border' : ingre_test_boarder[7]}" src="@/assets/img/material_card/card_7.png" @click="click_test_ingredient_check(name)" />
                                        <span>{{ value }} 장</span>
                                    </span>
                                    <span class="card_and_count" v-else-if="name == 'card_8' && value > 0" >
                                        <img class="sell_card_img" :class="{ 'test_border' : ingre_test_boarder[8]}" src="@/assets/img/material_card/card_8.png" @click="click_test_ingredient_check(name)" />
                                        <span>{{ value }} 장</span>
                                    </span>

                                </b-col>
                            </b-row>
                        </b-container>
                    </div>
                    <br>
                </div>
                <div>
                    <div id="make_potion_ing_btn_area">
                        <b-button pill id="my_reasoning_table_open" v-on:click="reasoning_table_open">
                            추리 테이블 보기
                        </b-button>
                        <b-button variant="outline-success" class="make_potion_dicision" v-on:click="make_potion_preparation">
                            조제하기
                        </b-button>
                    </div>
                </div> 
            </div>
        </b-modal>
    </div>
</template>

<style scoped src="@/assets/css/adventurer_selling.css">
</style>


<script src="@/assets/script/adventurer_selling.js">
</script>
