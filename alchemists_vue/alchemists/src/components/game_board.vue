<template>
<keep-alive>
    <div>
        <b-container fluid  id="game_board_container">
            <b-row>
                <b-col id="left_menu" cols="3">
                    <b-row id="room_title_back">
                        <b-col>
                            <div class="room_title">
                                알케미스트
                            </div>
                        </b-col>
                    </b-row>
                    <b-row>
                        <b-col>
                            <b-button pill id="my_item_open_button" data-bs-toggle="offcanvas" data-bs-target="#my_item" aria-controls="my_item">
                                내 아이템 보기
                            </b-button>
                        </b-col>
                        <b-col>
                            <b-button pill id="my_reasoning_table_open" v-on:click="reasoning_table_open">
                                추리 테이블 보기
                            </b-button>
                        </b-col>
                    </b-row>
                    <b-row>
                        <b-col>
                            <br>
                        </b-col>
                    </b-row>
                    <!-- 참여 유저의 공개정보 -->
                    <b-row align-v="stretch" v-for="i in Math.ceil(this.user_data.length / 2 )" :key="i">
                        <b-col class="show_entrans_data" sm="6" v-for="entrans in this.user_data.slice((i - 1) * 2, i * 2)" :key="entrans">
                            <b-overlay v-bind:show="entrans.is_ready == true">
                            <b-row>
                                <b-col>
                                    <div class="user_name_title">
                                        <span v-if="entrans.is_master == 'true'">
                                            방장
                                        </span>
                                        {{ entrans.user_name }}
                                        <img class="img_for_mine" v-if="entrans.user_key == this.my_key" src="@/assets/img/stamp/mine.png">
                                    </div>
                                </b-col>
                            </b-row>
                            <b-row>
                                <b-col>
                                    현재 소지한 금화 : {{ entrans.user_ingame_data.my_gold }}
                                </b-col>
                            </b-row>
                            <b-row>
                                <b-col>
                                    현재 점수 : {{ entrans.user_ingame_data.point }}
                                </b-col>
                            </b-row>
                            <b-row>
                                <b-col>
                                    현재 소지한 재료카드 : {{ entrans.user_ingame_data.ingredient.total }} 장
                                </b-col>
                            </b-row>
                            <b-row>
                                <b-col>
                                    현재 소지한 호의카드 : {{ entrans.user_ingame_data.favor_card.total }} 장
                                </b-col>
                            </b-row>
                            <b-row>
                                <b-col>
                                    현재 소지한 할인카드 : {{ this.total_dicount_adventruer }} 장
                                </b-col>
                            </b-row>
                            <b-row>
                                <b-col>
                                    현재 인장 : {{ entrans.user_ingame_data.have_stamp.total }} 개
                                </b-col>
                            </b-row>
                            <b-row >
                                <b-col>
                                    큐브
                                </b-col>
                                <b-col>
                                    <span v-if="entrans.user_color === 'red' " >
                                        <img v-for="n in entrans.user_ingame_data.cube_count" :key="n" class="cube_icon" src="@/assets/img/game_icon/cube_red.png">
                                    </span>
                                    <span v-if="entrans.user_color === 'blue' ">
                                        <img v-for="n in entrans.user_ingame_data.cube_count" :key="n" class="cube_icon" src="@/assets/img/game_icon/cube_blue.png">
                                    </span>
                                    <span v-if="entrans.user_color === 'black' ">
                                        <img v-for="n in entrans.user_ingame_data.cube_count" :key="n" class="cube_icon" src="@/assets/img/game_icon/cube_black.png">
                                    </span> 
                                    <span v-if="entrans.user_color === 'white' ">
                                        <img v-for="n in entrans.user_ingame_data.cube_count" :key="n" class="cube_icon" src="@/assets/img/game_icon/cube_white.png">
                                    </span>
                                </b-col>
                            </b-row>
                            <b-row>
                                <b-col>
                                    <img class="check_potion_area" v-if="entrans.user_ingame_data.is_check_potion.red_1 == true "
                                        src="@/assets/img/game_icon/red_+.png">
                                    <img class="check_potion_area" v-else 
                                        src="@/assets/img/game_icon/red_+_back.png">
                                </b-col>
                                <b-col>
                                    <img class="check_potion_area" v-if="entrans.user_ingame_data.is_check_potion.blue_1 == true "
                                        src="@/assets/img/game_icon/blue_+.png">
                                    <img class="check_potion_area" v-else 
                                        src="@/assets/img/game_icon/blue_+_back.png">
                                </b-col>
                                <b-col>
                                    <img class="check_potion_area" v-if="entrans.user_ingame_data.is_check_potion.green_1 == true "
                                        src="@/assets/img/game_icon/green_+.png">
                                    <img class="check_potion_area" v-else 
                                        src="@/assets/img/game_icon/green_+_back.png">
                                </b-col>
                            </b-row>
                            <b-row>
                                <b-col>
                                    <img class="check_potion_area" v-if="entrans.user_ingame_data.is_check_potion.red_0 == true "
                                        src="@/assets/img/game_icon/red_-.png">
                                    <img class="check_potion_area" v-else 
                                        src="@/assets/img/game_icon/red_-_back.png">
                                </b-col>
                                <b-col>
                                    <img class="check_potion_area" v-if="entrans.user_ingame_data.is_check_potion.blue_0 == true "
                                        src="@/assets/img/game_icon/blue_-.png">
                                    <img class="check_potion_area" v-else 
                                        src="@/assets/img/game_icon/blue_-_back.png">
                                </b-col>
                                <b-col>
                                    <img class="check_potion_area" v-if="entrans.user_ingame_data.is_check_potion.green_0 == true "
                                        src="@/assets/img/game_icon/green_-.png">
                                    <img class="check_potion_area" v-else 
                                        src="@/assets/img/game_icon/green_-_back.png">
                                </b-col>
                            </b-row>
                            <b-row>
                                <b-col>
                                    <img class="check_potion_area" v-if="entrans.user_ingame_data.is_check_potion.blank == true "
                                        src="@/assets/img/game_icon/blank.png">
                                    <img class="check_potion_area" v-else 
                                        src="@/assets/img/game_icon/blank_back.png">
                                </b-col>
                            </b-row>

                             <template v-slot:overlay>
                                <div class="overlay_ready">
                                    <b-icon-funnel style="width:100px; height:100px;" />
                                    <p id="check_label">준비 완료</p>
                                </div>
                            </template>
                            </b-overlay>

                        </b-col>
                    </b-row>

                    <!-- 채팅 -->
                    <b-row id="chat_area">
                        <b-col>
                            <div id="chat_title"> {{ this.room_owner }} 의 채팅방 </div>
                            <b-row>
                                <b-col>
                                    <textarea disabled ref="show_chat" v-model="textarea" rows=15 id="message_area">
                                    </textarea>
                                </b-col>
                            </b-row>
                            <b-row>
                                <b-col>
                                    <input v-model="message" placeholder="채팅을 입력하세요" @keyup.enter="use_chat"/>
                                    <b-button id="chat_submit_button" pill @click="use_chat">전송</b-button>
                                </b-col>
                            </b-row>
                        </b-col>
                    </b-row>
                </b-col>
                
                <b-col>
                    <b-overlay no-center v-bind:show="this.decide_order == false">
                        <div id="ready_btn_area">
                            <div id="ready_btn" @click="round_ready"> 현재 라운드 <br>준비 완료 </div>
                        </div>
                        <!-- 큐브 룰 표기 -->
                        <b-row class="part_row">
                            <b-col>
                                <img class="rule_img_show" src="@/assets/img/game_icon/cube_rule_1.png" v-b-modal.fame_of_prove>
                                <img class="rule_img_show" src="@/assets/img/game_icon/cube_rule_2.png" v-b-modal.none_use_cube>
                                <!-- img class="rule_img_show" src="@/assets/img/game_icon/cube_rule_3.png" v-b-modal.hospital -->
                            </b-col>
                            <b-col id="round_shower" cols="1">
                                현재 라운드 <br>
                                <span id="round_cont_span">{{ this.round_cont }}</span>
                            </b-col>
                            <b-col id="none_use_cube_area">
                                <span id="none_use_cube_area_label"> 미 사용 큐브 </span>
                                <b-row v-for="ent in this.user_data" :key="ent">
                                    <b-col>
                                    <span v-if="ent.user_color === 'red' " >
                                        <img v-for="n in ent.user_ingame_data.cube_count" :key="n" class="cube_icon" src="@/assets/img/game_icon/cube_red.png">
                                    </span>
                                    <span v-if="ent.user_color === 'blue' ">
                                        <img v-for="n in ent.user_ingame_data.cube_count" :key="n" class="cube_icon" src="@/assets/img/game_icon/cube_blue.png">
                                    </span>
                                    <span v-if="ent.user_color === 'black' ">
                                        <img v-for="n in ent.user_ingame_data.cube_count" :key="n" class="cube_icon" src="@/assets/img/game_icon/cube_black.png">
                                    </span>
                                    <span v-if="ent.user_color === 'white' ">
                                        <img v-for="n in ent.user_ingame_data.cube_count" :key="n" class="cube_icon" src="@/assets/img/game_icon/cube_white.png">
                                    </span>
                                    </b-col>
                                </b-row>
                            </b-col>
                            <b-col id="hospital_area">

                            </b-col>
                        </b-row>

                        <!-- 재료카드 뽑기 -->
                        <b-row id="get_ingredient" class="part_row board" :class="{'now_board' : this.board_1_selected}">
                            <b-col>
                                <img class="part_icon" src="@/assets/img/game_icon/select_items_icon.png">
                            </b-col>
                            <b-col cols="1">
                                <img @click="pick_ingredient(0, 0)" class="ingredient_select_card" src="@/assets/img/material_card/card_back.png">
                            </b-col>
                            <b-col cols="1" v-for="(card, index) in this.ingredient_card_selected" :key="card">
                                <img v-if="card == '1'"      @click="pick_ingredient(card, index)" class="ingredient_select_card" src="@/assets/img/material_card/card_1.png">
                                <img v-else-if="card == '2'" @click="pick_ingredient(card, index)" class="ingredient_select_card" src="@/assets/img/material_card/card_2.png">
                                <img v-else-if="card == '3'" @click="pick_ingredient(card, index)" class="ingredient_select_card" src="@/assets/img/material_card/card_3.png">
                                <img v-else-if="card == '4'" @click="pick_ingredient(card, index)" class="ingredient_select_card" src="@/assets/img/material_card/card_4.png">
                                <img v-else-if="card == '5'" @click="pick_ingredient(card, index)" class="ingredient_select_card" src="@/assets/img/material_card/card_5.png">
                                <img v-else-if="card == '6'" @click="pick_ingredient(card, index)" class="ingredient_select_card" src="@/assets/img/material_card/card_6.png">
                                <img v-else-if="card == '7'" @click="pick_ingredient(card, index)" class="ingredient_select_card" src="@/assets/img/material_card/card_7.png">
                                <img v-else-if="card == '8'" @click="pick_ingredient(card, index)" class="ingredient_select_card" src="@/assets/img/material_card/card_8.png">
                                <img v-else @click="pick_ingredient(0, index)" class="ingredient_select_card" src="@/assets/img/material_card/card_back.png">
                            </b-col>
                            <b-col>
                                <br>
                                <b-row v-for="user in this.user_cube_data[1]" :key="user">
                                    <b-col v-for="n in user.cube" :key="n">
                                        <span v-for="u in n.cnt" :key="u">
                                            <button class="color_select_btn" 
                                                    :class="[user.user_color , {'selected' : n.is_select}]"
                                                    @click="click_order_btn(1, n.num, user.user_key)" 
                                                    >
                                            </button>
                                        </span>
                                        <br><br><br>
                                    </b-col>
                                </b-row>
                            </b-col>
                        </b-row>

                        <!-- 재료카드 판매 -->
                        <b-row class="part_row board" :class="{'now_board' : this.board_2_selected}">
                            <b-col cols="3">
                                <img class="part_icon too_big" src="@/assets/img/game_icon/sell_item_icon.png">
                            </b-col>
                            <b-col cols="5">
                                <br>
                                <b-row v-for="user in this.user_cube_data[2]" :key="user">
                                    <b-col v-for="n in user.cube" :key="n">
                                        <span v-for="u in n.cnt" :key="u">
                                            <button class="color_select_btn" 
                                                    :class="[user.user_color , {'selected' : n.is_select}]"
                                                    @click="click_order_btn(2, n.num, user.user_key)" 
                                                    >
                                            </button>
                                        </span>
                                        <br><br><br>
                                    </b-col>
                                </b-row>
                            </b-col>
                        </b-row>

                        <!-- 용병에게 물약 판매 -->
                        <b-row class="part_row board" :class="{'now_board' : this.board_3_selected}">
                            <b-col cols="4" v-if="this.round_cont == 1">
                                <img class="part_icon" style="height:65%;" src="@/assets/img/adventurer/adventurer_back.png">
                                <img id="adventurer_coin" style="height:70%;" src="@/assets/img/adventurer/adventurer_coin.png">
                            </b-col>
                            <b-col cols="4" v-else-if="this.round_cont == 2" >
                                <img class="part_icon"      v-if="this.random_adv_list[0] == 1" style="height:65%;" src="@/assets/img/adventurer/adventurer_1.png">
                                <img class="part_icon" v-else-if="this.random_adv_list[0] == 2" style="height:65%;" src="@/assets/img/adventurer/adventurer_2.png">
                                <img class="part_icon" v-else-if="this.random_adv_list[0] == 3" style="height:65%;" src="@/assets/img/adventurer/adventurer_3.png">
                                <img class="part_icon" v-else-if="this.random_adv_list[0] == 4" style="height:65%;" src="@/assets/img/adventurer/adventurer_4.png">
                                <img class="part_icon" v-else-if="this.random_adv_list[0] == 5" style="height:65%;" src="@/assets/img/adventurer/adventurer_5.png">
                                <img class="part_icon" v-else-if="this.random_adv_list[0] == 6" style="height:65%;" src="@/assets/img/adventurer/adventurer_6.png">
                                <img id="adventurer_coin" style="height:70%;" src="@/assets/img/adventurer/adventurer_coin.png">
                            </b-col>
                            <b-col cols="4" v-else-if="this.round_cont == 3" >
                                <img class="part_icon"      v-if="this.random_adv_list[1] == 1" style="height:65%;" src="@/assets/img/adventurer/adventurer_1.png">
                                <img class="part_icon" v-else-if="this.random_adv_list[1] == 2" style="height:65%;" src="@/assets/img/adventurer/adventurer_2.png">
                                <img class="part_icon" v-else-if="this.random_adv_list[1] == 3" style="height:65%;" src="@/assets/img/adventurer/adventurer_3.png">
                                <img class="part_icon" v-else-if="this.random_adv_list[1] == 4" style="height:65%;" src="@/assets/img/adventurer/adventurer_4.png">
                                <img class="part_icon" v-else-if="this.random_adv_list[1] == 5" style="height:65%;" src="@/assets/img/adventurer/adventurer_5.png">
                                <img class="part_icon" v-else-if="this.random_adv_list[1] == 6" style="height:65%;" src="@/assets/img/adventurer/adventurer_6.png">
                                <img id="adventurer_coin" style="height:70%;" src="@/assets/img/adventurer/adventurer_coin.png">
                            </b-col>
                            <b-col cols="4" v-else-if="this.round_cont == 4" >
                                <img class="part_icon"      v-if="this.random_adv_list[2] == 1" style="height:65%;" src="@/assets/img/adventurer/adventurer_1.png">
                                <img class="part_icon" v-else-if="this.random_adv_list[2] == 2" style="height:65%;" src="@/assets/img/adventurer/adventurer_2.png">
                                <img class="part_icon" v-else-if="this.random_adv_list[2] == 3" style="height:65%;" src="@/assets/img/adventurer/adventurer_3.png">
                                <img class="part_icon" v-else-if="this.random_adv_list[2] == 4" style="height:65%;" src="@/assets/img/adventurer/adventurer_4.png">
                                <img class="part_icon" v-else-if="this.random_adv_list[2] == 5" style="height:65%;" src="@/assets/img/adventurer/adventurer_5.png">
                                <img class="part_icon" v-else-if="this.random_adv_list[2] == 6" style="height:65%;" src="@/assets/img/adventurer/adventurer_6.png">
                                <img id="adventurer_coin" style="height:70%;" src="@/assets/img/adventurer/adventurer_coin.png">
                            </b-col>
                            <b-col cols="4" v-else-if="this.round_cont == 5" >
                                <img class="part_icon"      v-if="this.random_adv_list[3] == 1" style="height:65%;" src="@/assets/img/adventurer/adventurer_1.png">
                                <img class="part_icon" v-else-if="this.random_adv_list[3] == 2" style="height:65%;" src="@/assets/img/adventurer/adventurer_2.png">
                                <img class="part_icon" v-else-if="this.random_adv_list[3] == 3" style="height:65%;" src="@/assets/img/adventurer/adventurer_3.png">
                                <img class="part_icon" v-else-if="this.random_adv_list[3] == 4" style="height:65%;" src="@/assets/img/adventurer/adventurer_4.png">
                                <img class="part_icon" v-else-if="this.random_adv_list[3] == 5" style="height:65%;" src="@/assets/img/adventurer/adventurer_5.png">
                                <img class="part_icon" v-else-if="this.random_adv_list[3] == 6" style="height:65%;" src="@/assets/img/adventurer/adventurer_6.png">
                                <img id="adventurer_coin" style="height:70%;" src="@/assets/img/adventurer/adventurer_coin.png">
                            </b-col>
                            <b-col cols="4" v-else-if="this.round_cont == 6" >
                                <img class="part_icon"      v-if="this.random_adv_list[4] == 1" style="height:65%;" src="@/assets/img/adventurer/adventurer_1.png">
                                <img class="part_icon" v-else-if="this.random_adv_list[4] == 2" style="height:65%;" src="@/assets/img/adventurer/adventurer_2.png">
                                <img class="part_icon" v-else-if="this.random_adv_list[4] == 3" style="height:65%;" src="@/assets/img/adventurer/adventurer_3.png">
                                <img class="part_icon" v-else-if="this.random_adv_list[4] == 4" style="height:65%;" src="@/assets/img/adventurer/adventurer_4.png">
                                <img class="part_icon" v-else-if="this.random_adv_list[4] == 5" style="height:65%;" src="@/assets/img/adventurer/adventurer_5.png">
                                <img class="part_icon" v-else-if="this.random_adv_list[4] == 6" style="height:65%;" src="@/assets/img/adventurer/adventurer_6.png">
                                <img id="adventurer_coin" style="height:70%;" src="@/assets/img/adventurer/adventurer_coin.png">
                            </b-col>
                            <b-col cols="2">
                                <br>
                                <b-row v-for="user in this.user_cube_data[3]" :key="user">
                                    <b-col v-for="n in user.cube" :key="n">
                                        <span v-for="u in n.cnt" :key="u">
                                            <button class="color_select_btn" 
                                                    :class="[user.user_color , {'selected' : n.is_select}]"
                                                    @click="click_order_btn(3, n.num, user.user_key)" 
                                                    >
                                            </button>
                                        </span>
                                        <br><br><br>
                                    </b-col>
                                </b-row>
                            </b-col>
                        </b-row>

                        <!-- 아티팩트 구매 -->
                        <b-row class="part_row" :class="{'now_board' : this.board_4_selected}">
                            <b-col cols="3">
                                <img class="part_icon too_big" src="@/assets/img/game_icon/buy_item_icon.png">
                            </b-col>
                            <b-col cols="3">
                                <br>
                                <b-row v-for="user in this.user_cube_data[4]" :key="user">
                                    <b-col v-for="n in user.cube" :key="n">
                                        <span v-for="u in n.cnt" :key="u">
                                            <button class="color_select_btn" 
                                                    :class="[user.user_color , {'selected' : n.is_select}]"
                                                    @click="click_order_btn(4, n.num, user.user_key)" 
                                                    >
                                            </button>
                                        </span>
                                        <br><br><br>
                                    </b-col>
                                </b-row>
                            </b-col>
                            <b-col v-for="arti in this.can_buy_artifacts.show_artifacts" :key="arti">
                                <img v-if="this.can_buy_artifacts.rank == 1 && arti == 0 "      class="artifacts_icon_card" src="@/assets/img/artifacts/rank_1/discount_card.png" v-b-modal.discount_card>
                                <img v-else-if="this.can_buy_artifacts.rank == 1 && arti == 1 " class="artifacts_icon_card" src="@/assets/img/artifacts/rank_1/haste_boots.png" v-b-modal.haste_boots>
                                <img v-else-if="this.can_buy_artifacts.rank == 1 && arti == 2 " class="artifacts_icon_card" src="@/assets/img/artifacts/rank_1/magic_mortar.png" v-b-modal.magic_mortar>
                                <img v-else-if="this.can_buy_artifacts.rank == 1 && arti == 3 " class="artifacts_icon_card" src="@/assets/img/artifacts/rank_1/night_vision.png" v-b-modal.night_vision>
                                <img v-else-if="this.can_buy_artifacts.rank == 1 && arti == 4 " class="artifacts_icon_card" src="@/assets/img/artifacts/rank_1/printing_machine.png" v-b-modal.printing_machine>
                                <img v-else-if="this.can_buy_artifacts.rank == 1 && arti == 5 " class="artifacts_icon_card" src="@/assets/img/artifacts/rank_1/robe_of_respect.png" v-b-modal.robe_of_respect>
                                <img v-else-if="this.can_buy_artifacts.rank == 1 && arti == -1 " class="artifacts_icon_card" src="@/assets/img/artifacts/rank_1/rank_1_back.png" >
                                
                                <img v-else-if="this.can_buy_artifacts.rank == 2 && arti == 0 " class="artifacts_icon_card" src="@/assets/img/artifacts/rank_2/chest_of_witch.png" v-b-modal.chest_of_witch>
                                <img v-else-if="this.can_buy_artifacts.rank == 2 && arti == 1 " class="artifacts_icon_card" src="@/assets/img/artifacts/rank_2/eloquent_necklace.png" v-b-modal.eloquent_necklace>
                                <img v-else-if="this.can_buy_artifacts.rank == 2 && arti == 2 " class="artifacts_icon_card" src="@/assets/img/artifacts/rank_2/hypnotic_necklace.png" v-b-modal.hypnotic_necklace>
                                <img v-else-if="this.can_buy_artifacts.rank == 2 && arti == 3 " class="artifacts_icon_card" src="@/assets/img/artifacts/rank_2/seal_of_authority.png" v-b-modal.seal_of_authority>
                                <img v-else-if="this.can_buy_artifacts.rank == 2 && arti == 4 " class="artifacts_icon_card" src="@/assets/img/artifacts/rank_2/silver_glass.png" v-b-modal.silver_glass>
                                <img v-else-if="this.can_buy_artifacts.rank == 2 && arti == 5 " class="artifacts_icon_card" src="@/assets/img/artifacts/rank_2/thinking_hat.png" v-b-modal.thinking_hat>
                                <img v-else-if="this.can_buy_artifacts.rank == 2 && arti == -1 " class="artifacts_icon_card" src="@/assets/img/artifacts/rank_2/rank_2_back.png" >

                                <img v-else-if="this.can_buy_artifacts.rank == 3 && arti == 0 " class="artifacts_icon_card" src="@/assets/img/artifacts/rank_3/bronze_cup.png" v-b-modal.bronze_cup>
                                <img v-else-if="this.can_buy_artifacts.rank == 3 && arti == 1 " class="artifacts_icon_card" src="@/assets/img/artifacts/rank_3/feather_hat.png" v-b-modal.feather_hat>
                                <img v-else-if="this.can_buy_artifacts.rank == 3 && arti == 2 " class="artifacts_icon_card" src="@/assets/img/artifacts/rank_3/glass_cabinet.png" v-b-modal.glass_cabinet>
                                <img v-else-if="this.can_buy_artifacts.rank == 3 && arti == 3 " class="artifacts_icon_card" src="@/assets/img/artifacts/rank_3/golden_alter.png" v-b-modal.golden_alter>
                                <img v-else-if="this.can_buy_artifacts.rank == 3 && arti == 4 " class="artifacts_icon_card" src="@/assets/img/artifacts/rank_3/magic_mirror.png" v-b-modal.magic_mirror>
                                <img v-else-if="this.can_buy_artifacts.rank == 3 && arti == 5 " class="artifacts_icon_card" src="@/assets/img/artifacts/rank_3/statue_of_wisdom.png" v-b-modal.statue_of_wisdom>
                                <img v-else-if="this.can_buy_artifacts.rank == 3 && arti == -1 " class="artifacts_icon_card" src="@/assets/img/artifacts/rank_3/rank_3_back.png" >
                            </b-col>
                        </b-row>

                        <!-- 논문 반박하기 -->
                        <b-row class="part_row" :class="{'now_board' : this.board_5_selected}">
                            <b-col cols="3">
                                <img class="part_icon" src="@/assets/img/game_icon/refuting_a_theory_icon.png">
                            </b-col>
                            <b-col cols="5">
                                <br>
                                <b-row v-for="user in this.user_cube_data[5]" :key="user">
                                    <b-col v-for="n in user.cube" :key="n">
                                        <span v-for="u in n.cnt" :key="u">
                                            <button class="color_select_btn" 
                                                    :class="[user.user_color , {'selected' : n.is_select}]"
                                                    @click="click_order_btn(5, n.num, user.user_key)" 
                                                    >
                                            </button>
                                        </span>
                                        <br><br><br>
                                    </b-col>
                                </b-row>
                            </b-col>
                        </b-row>

                        <!-- 논문 발표 -->
                        <b-row class="part_row" :class="{'now_board' : this.board_6_selected}">
                            <b-col cols="3">
                                <img class="part_icon" src="@/assets/img/game_icon/presentation_of_theories_icon.png">
                            </b-col>
                            <b-col cols="5">
                                <br>
                                <b-row v-for="user in this.user_cube_data[6]" :key="user">
                                    <b-col v-for="n in user.cube" :key="n">
                                        <span v-for="u in n.cnt" :key="u">
                                            <button class="color_select_btn" 
                                                    :class="[user.user_color , {'selected' : n.is_select}]"
                                                    @click="click_order_btn(6, n.num, user.user_key)" 
                                                    >
                                            </button>
                                        </span>
                                        <br><br><br>
                                    </b-col>
                                </b-row>
                            </b-col>
                        </b-row>

                        <!-- 조수에게 물약 시험 -->
                        <b-row class="part_row" :class="{'now_board' : this.board_7_selected}">
                            <b-col cols="3">
                                <img class="part_icon" src="@/assets/img/game_icon/test_student_icon.png">
                            </b-col>
                            <b-col cols="5">
                                <br>
                                <b-row v-for="user in this.user_cube_data[7]" :key="user">
                                    <b-col v-for="n in user.cube" :key="n">
                                        <span v-for="u in n.cnt" :key="u">
                                            <button class="color_select_btn" 
                                                    :class="[user.user_color , {'selected' : n.is_select}]"
                                                    @click="click_order_btn(7, n.num, user.user_key)" 
                                                    >
                                            </button>
                                        </span>
                                        <br><br><br>
                                    </b-col>
                                </b-row>
                            </b-col>
                        </b-row>

                        <!-- 스스로 뭉약 실험 -->
                        <b-row class="part_row" :class="{'now_board' : this.board_8_selected}">
                            <b-col cols="3">
                                <img class="part_icon" src="@/assets/img/game_icon/test_i_icon.png">
                            </b-col>
                            <b-col cols="5">
                                <br>
                                <b-row v-for="user in this.user_cube_data[8]" :key="user">
                                    <b-col v-for="n in user.cube" :key="n">
                                        <span v-for="u in n.cnt" :key="u">
                                            <button class="color_select_btn" 
                                                    :class="[user.user_color , {'selected' : n.is_select}]"
                                                    @click="click_order_btn(8, n.num, user.user_key)" 
                                                    >
                                            </button>
                                        </span>
                                        <br><br><br>
                                    </b-col>
                                </b-row>
                            </b-col>
                        </b-row>

                        <!-- 마지막 라운드 물약 시연 칸 -->
                        <b-row v-if="this.round_cont == '6'" class="part_row" :class="{'now_board' : this.board_end_selected}">
                            <b-col cols="3">
                                <!-- 이미지 자리 -->
                                <img class="part_icon" src="@/assets/img/game_icon/exhibition.png">
                            </b-col>
                            <!-- 큐브 1111 -->
                            <b-col cols="5">
                                <br>
                                <b-row v-for="user in this.user_cube_data[9]" :key="user">
                                    <b-col v-for="n in user.cube" :key="n">
                                        <span v-for="u in n.cnt" :key="u">
                                            <button class="color_select_btn" 
                                                    :class="[user.user_color , {'selected' : n.is_select}]"
                                                    @click="click_order_btn(9, n.num, user.user_key)" 
                                                    >
                                            </button>
                                        </span>
                                        <br><br><br>
                                    </b-col>
                                </b-row>
                            </b-col>
                        </b-row>

                    <template #overlay>
                        <div class="text-center" style="left: 50%;">
                            <p id="decide_order_title">순서 정하기</p>
                            <b-container style="width:35%;">
                                <b-row>
                                    <b-col>
                                        <b-row>
                                            <b-col id="button_p_col">
                                                <p class="button_p" v-for="n in 8" :key="n">
                                                    <button class="order_select_table_btn" :ref="'btn_'+n" v-on:click="select_order_btn(n, $event)"></button>
                                                </p>
                                            </b-col>
                                            <b-col>
                                                <img id="order_select_table_img" src="@/assets/img/game_icon/order_select_table.png">
                                            </b-col>
                                            <b-col>
                                                <span>선택 <br>순서</span>
                                                <br><br>
                                                <p class="round_order_selecting_name" v-for="user in this.round_setting_order" :key="user">
                                                    <span >{{ user.user_name }}</span>
                                                </p>
                                            </b-col>
                                        </b-row>
                                    </b-col>
                                </b-row>
                            </b-container>
                            <b-button variant="outline-primary" block @click="decide_order_modal_open">완료</b-button>
                        </div>
                    </template>
                    </b-overlay>
                </b-col>
            </b-row>
        </b-container>

        <!-- 라운드 순서 결정하는 offcanvas -->
        <b-modal id="decide_order_mine" 
                centered 
                v-model="decide_order_modal"
                ref="decide_order_mine" 
                header-text-variant="light"
                header-bg-variant="danger"
                hide-footer
                hide-header-close
                title="확인창"
                >
            <div id="decide_order_mine_modal_intitle">
               <p>선택한 순서를 결정하시겠습니까?</p>
               <p>결정되면 다시 선택 할 수 없습니다</p>
            </div>
            <div id="decide_order_button_area">
                <b-button variant="outline-success" block class="decide_order_button" v-on:click="decide_order_setting_end">결정</b-button>
                <b-button variant="outline-danger"  block class="decide_order_button" data-bs-dismiss="modal">취소</b-button>
            </div> 
        </b-modal>

        <!-- 라운드 순서 결정 모달 -->
        <b-modal id="round_order_check" 
                centered 
                v-model="round_order_check"
                ref="round_order_check" 
                header-text-variant="light"
                header-bg-variant="danger"
                hide-footer
                hide-header-close
                title="확인창"
                >
            <div id="round_order_check_intitle">
               <p>선택한 큐브로 라운드 진행을 결정하시겠습니까?</p>
               <p>결정되면 다시 선택 할 수 없습니다</p>
            </div>
            <div id="round_order_check_btn_area">
                <b-button variant="outline-success" block class="round_order_button" v-on:click="round_order_setting_end">결정</b-button>
                <b-button variant="outline-danger"  block class="round_order_button" v-on:click="round_order_setting_close">취소</b-button>
            </div> 
        </b-modal>

        <!-- 호의 카드 사용 모달 -->
        <Favor_card_use
            ref="favor_card_use_check_modal"
            :key="this.favor_card_use_key_val"
            v-bind:my_data="this.my_data"
            v-bind:temp_used_favor_card_list="this.temp_used_favor_card_list"
            v-on:favor_card_use_check="favor_card_use_check"
        >
        </Favor_card_use>

        <!-- 재료 판매 모달 -->
        <b-modal id="sell_ingredient_modal" 
            lazy
            centered 
            size="lg"
            ref="sell_ingredient_modal"
            v-model="sell_ingredient_modal_onoff"
            header-text-variant="light"
            header-bg-variant="danger"
            hide-footer
            hide-header-close
            hide-backdrop
            no-close-on-backdrop
            title="판매 가능 목록"
            >
            <div v-if="this.my_turn == true">
                <div class="modal_in">
                    <p>어떤 것을 고르시겠습니까?</p>
                    <div class="select_area">
                        <div>
                            <b-container>
                                <b-row v-if="this.my_data.user_key == this.my_key ">
                                    <b-col v-for="( card , name ) in this.my_data.user_ingame_data.ingredient" :key="card">

                                        <span class="card_and_count" v-if="name == 'card_1' && card > 0" >
                                            <img class="sell_card_img" ref="sell_card_1" src="@/assets/img/material_card/card_1.png" @click="click_sell_ingredient_check(name)">
                                            <span> {{ card }} 장</span>
                                        </span>
                                        <span class="card_and_count" v-else-if="name == 'card_2' && card > 0" >
                                            <img class="sell_card_img" ref="sell_card_2" src="@/assets/img/material_card/card_2.png" @click="click_sell_ingredient_check(name)">
                                            <span>{{ card }} 장</span>
                                        </span>
                                        <span class="card_and_count" v-else-if="name == 'card_3' && card > 0" >
                                            <img class="sell_card_img" ref="sell_card_3" src="@/assets/img/material_card/card_3.png" @click="click_sell_ingredient_check(name)">
                                            <span>{{ card }} 장</span>
                                        </span>
                                        <span class="card_and_count" v-else-if="name == 'card_4' && card > 0" >
                                            <img class="sell_card_img" ref="sell_card_4" src="@/assets/img/material_card/card_4.png" @click="click_sell_ingredient_check(name)">
                                            <span>{{ card }} 장</span>
                                        </span>
                                        <span class="card_and_count" v-else-if="name == 'card_5' && card > 0" >
                                            <img class="sell_card_img" ref="sell_card_5" src="@/assets/img/material_card/card_5.png" @click="click_sell_ingredient_check(name)">
                                            <span>{{ card }} 장</span>
                                        </span>
                                        <span class="card_and_count" v-else-if="name == 'card_6' && card > 0" >
                                            <img class="sell_card_img" ref="sell_card_6" src="@/assets/img/material_card/card_6.png" @click="click_sell_ingredient_check(name)">
                                            <span>{{ card }} 장</span>
                                        </span>
                                        <span class="card_and_count" v-else-if="name == 'card_7' && card > 0" >
                                            <img class="sell_card_img" ref="sell_card_7" src="@/assets/img/material_card/card_7.png" @click="click_sell_ingredient_check(name)">
                                            <span>{{ card }} 장</span>
                                        </span>
                                        <span class="card_and_count" v-else-if="name == 'card_8' && card > 0" >
                                            <img class="sell_card_img" ref="sell_card_8" src="@/assets/img/material_card/card_8.png" @click="click_sell_ingredient_check(name)">
                                            <span>{{ card }} 장</span>
                                        </span>

                                    </b-col>
                                </b-row>
                            </b-container>
                        </div>
                    </div>
                    <br>
                </div>
                <div>
                    <p id="sell_ingredient_announce">
                        한 번 결정을 누르면 알림창 없이 바로 판매됩니다.<br>
                        신중히 결정해주세요
                    </p>
                    <div id="sell_ingredient_btn_area">
                        <b-button variant="outline-success" class="reasoning_table_button" v-on:click="click_sell_ingredient">
                            결정
                        </b-button>
                        <b-button variant="outline-danger" class="reasoning_table_button" data-bs-dismiss="modal" v-on:click="end_selling_ingredient">취소</b-button>
                    </div>
                </div> 
            </div>
            <div v-else>
                <div class="modal_out">
                    <div class="not_my_turn">
                        <b-spinner label="Spinning"></b-spinner>
                        <p>
                            다른 사람이 선택 중 입니다. 잠시 기다려주세요
                        </p>
                        <p>
                            <b-button pill id="my_reasoning_table_open" v-on:click="reasoning_table_open">
                                추리 테이블 보기
                            </b-button>
                        </p>
                    </div>
                </div>
            </div>
        </b-modal>

        <!-- 용병에게 물약 판매 모달 -->

        <Adventurer_selling
            :ref="'adventurer_selling_modal'+this.restart_counter"
            
            v-bind:round_cont="this.round_cont"
            v-bind:sell_potion_turn="this.sell_potion_turn"
            v-bind:adventurer_card_ori_data="this.adventurer_card_ori_data"
            v-bind:random_adv_list="this.random_adv_list"
            v-bind:discount_coin_list="this.discount_coin_list"
            v-bind:is_end_discount="this.is_end_discount"
            v-bind:im_end_discount="this.im_end_discount"
            v-bind:my_selling_price="this.my_selling_price"
            v-bind:selling_turn="this.selling_turn"
            v-bind:get_discount_coin="this.get_discount_coin"
            v-bind:what_kind_sell_potion="this.what_kind_sell_potion"
            v-on:open_reasoning_table_from_adv="reasoning_table_open"
            @hold_sell_potion_price="decision_sell_potion_price"
            @hold_before_adv_discount_num="hold_before_adv_discount_num"
            @kind_sell_potion_check="select_sell_potion_kind"
            @adv_dis_confirm="adv_dis_confirm"
            @sell_potion_set_price="sell_potion_set_price"
            @make_potion_check="make_potion_preparation"
        >
        </Adventurer_selling>


        <!-- 용병에게 판매한 결과 보이기 -->
        <b-modal id="selling_end_modal" 
            ref="selling_end_modal"
            v-model="selling_end_modal"
            header-text-variant="light"
            header-bg-variant="success"
            hide-footer
            hide-backdrop
            hide-header-close
            title="판매 결과"
            >
            <div v-if="this.selling_success == true">
                <b-container>
                    <b-row>
                        <b-col>
                            <span class="selling_user_ment" >
                                판매자 색  
                            </span>
                            <img class="selling_user_icon" v-if="this.selling_user_color == 'red' "        src="@/assets/img/game_icon/cube_red.png"  />
                            <img class="selling_user_icon" v-else-if="this.selling_user_color == 'blue' "  src="@/assets/img/game_icon/cube_blue.png"  />
                            <img class="selling_user_icon" v-else-if="this.selling_user_color == 'black' " src="@/assets/img/game_icon/cube_black.png"  />
                            <img class="selling_user_icon" v-else-if="this.selling_user_color == 'white' " src="@/assets/img/game_icon/cube_white.png"  />
                        </b-col>
                    </b-row>
                    <b-row>
                        <b-col class="selling_success_ment">
                            판매 성공
                        </b-col>
                    </b-row>
                    <b-row>
                        <span class="selling_potion_ment">
                            판매한 물약 
                        </span>
                        <b-col>
                            <img class="selling_success_icon" v-if="this.selling_potion_kind == 'red_1'" src="@/assets/img/game_icon/red_+.png" />
                            <img class="selling_success_icon" v-else-if="this.selling_potion_kind == 'red_0'" src="@/assets/img/game_icon/red_-.png" />
                            <img class="selling_success_icon" v-else-if="this.selling_potion_kind == 'green_1'" src="@/assets/img/game_icon/green_+.png" />
                            <img class="selling_success_icon" v-else-if="this.selling_potion_kind == 'green_0'" src="@/assets/img/game_icon/green_-.png" />
                            <img class="selling_success_icon" v-else-if="this.selling_potion_kind == 'blue_1'" src="@/assets/img/game_icon/blue_+.png" />
                            <img class="selling_success_icon" v-else-if="this.selling_potion_kind == 'blue_0'" src="@/assets/img/game_icon/blue_-.png" />
                        </b-col>
                    </b-row>
                </b-container>
            </div>
            <div v-else>
                 <b-container>
                    <b-row>
                        <b-col>
                            <span class="selling_user_ment" >
                                판매자 색  
                            </span>
                            <img class="selling_user_icon" v-if="this.selling_user_color == 'red' "        src="@/assets/img/game_icon/cube_red.png"  />
                            <img class="selling_user_icon" v-else-if="this.selling_user_color == 'blue' "  src="@/assets/img/game_icon/cube_blue.png"  />
                            <img class="selling_user_icon" v-else-if="this.selling_user_color == 'black' " src="@/assets/img/game_icon/cube_black.png"  />
                            <img class="selling_user_icon" v-else-if="this.selling_user_color == 'white' " src="@/assets/img/game_icon/cube_white.png"  />
                        </b-col>
                    </b-row>
                    <b-row>
                        <b-col class="selling_fail_ment">
                            판매 실패
                        </b-col>
                    </b-row>
                </b-container>
            </div>
        </b-modal>

        <!-- 물약 실험 모달 -->
        <b-modal id="test_ingredient_modal" 
            centered
            size="lg"
            ref="test_ingredient_modal"
            v-model="test_ingredient_modal_onoff"
            header-text-variant="light"
            header-bg-variant="primary"
            hide-footer
            hide-header-close
            hide-backdrop
            no-close-on-backdrop
            title="실험"
            >
            <div v-if="this.my_turn == true">
                <div class="modal_in">
                    <p v-if="this.board_7_selected == true "> 학생에게 실험하기 </p>
                    <p v-else-if="this.board_8_selected == true "> 스스로 실험하기 </p>
                    <p>어떤 것을 고르시겠습니까? <br> 서로 다른 2개로 실험 할 수 있습니다.</p>
                    <div class="select_area">
                        <div>
                            <b-container>
                                <b-row v-if="this.my_data.user_key == this.my_key ">
                                    <b-col v-for="( card , name ) in this.my_data.user_ingame_data.ingredient" :key="card">
                                        <span class="card_and_count" v-if="name == 'card_1' && card > 0" @click="click_test_ingredient_check(name)">
                                            <img class="sell_card_img" ref="test_card_1" src="@/assets/img/material_card/card_1.png"  />
                                            <span> {{ card }} 장</span>
                                        </span>
                                        <span class="card_and_count" v-else-if="name == 'card_2' && card > 0" @click="click_test_ingredient_check(name)">
                                            <img class="sell_card_img" ref="test_card_2" src="@/assets/img/material_card/card_2.png"  />
                                            <span>{{ card }} 장</span>
                                        </span>
                                        <span class="card_and_count" v-else-if="name == 'card_3' && card > 0" @click="click_test_ingredient_check(name)">
                                            <img class="sell_card_img" ref="test_card_3" src="@/assets/img/material_card/card_3.png"  />
                                            <span>{{ card }} 장</span>
                                        </span>
                                        <span class="card_and_count" v-else-if="name == 'card_4' && card > 0" @click="click_test_ingredient_check(name)">
                                            <img class="sell_card_img" ref="test_card_4" src="@/assets/img/material_card/card_4.png"  />
                                            <span>{{ card }} 장</span>
                                        </span>
                                        <span class="card_and_count" v-else-if="name == 'card_5' && card > 0" @click="click_test_ingredient_check(name)">
                                            <img class="sell_card_img" ref="test_card_5" src="@/assets/img/material_card/card_5.png"   />
                                            <span>{{ card }} 장</span>
                                        </span>
                                        <span class="card_and_count" v-else-if="name == 'card_6' && card > 0" @click="click_test_ingredient_check(name)" >
                                            <img class="sell_card_img" ref="test_card_6" src="@/assets/img/material_card/card_6.png"  />
                                            <span>{{ card }} 장</span>
                                        </span>
                                        <span class="card_and_count" v-else-if="name == 'card_7' && card > 0" @click="click_test_ingredient_check(name)">
                                            <img class="sell_card_img" ref="test_card_7" src="@/assets/img/material_card/card_7.png"  />
                                            <span>{{ card }} 장</span>
                                        </span>
                                        <span class="card_and_count" v-else-if="name == 'card_8' && card > 0" @click="click_test_ingredient_check(name)">
                                            <img class="sell_card_img" ref="test_card_8" src="@/assets/img/material_card/card_8.png"  />
                                            <span>{{ card }} 장</span>
                                        </span>

                                    </b-col>
                                </b-row>
                            </b-container>
                        </div>
                    </div>
                    <br>
                </div>
                <div>
                    <p id="sell_ingredient_announce">
                        한 번 결정을 누르면 알림창 없이 바로 실험 재료로 소모됩니다.<br>
                        신중히 결정해주세요
                    </p>
                    <p>
                        <b-button pill id="my_reasoning_table_open" v-on:click="reasoning_table_open">
                            추리 테이블 보기
                        </b-button>
                    </p>
                    <div id="sell_ingredient_btn_area">
                        <b-button variant="outline-success" class="reasoning_table_button" v-on:click="click_test_ingredient">
                            결정
                        </b-button>
                        <b-button variant="outline-danger" class="reasoning_table_button" v-on:click="pass_board">
                            차례 넘기기
                        </b-button>
                    </div>
                </div> 
            </div>
            <div v-else>
                <div class="modal_out">
                    <div class="not_my_turn">
                        <b-spinner label="Spinning"></b-spinner>
                        <p>
                            다른 사람이 실험 중 입니다. 잠시 기다려주세요
                        </p>
                        <p>
                            <b-button pill id="my_reasoning_table_open" v-on:click="reasoning_table_open">
                                추리 테이블 보기
                            </b-button>
                        </p>
                    </div>
                </div>
            </div>
        </b-modal>

        <!-- 실험 결과 announce -->
        <b-modal id="open_result_modal" 
            ref="open_result_modal"
            v-model="open_result_modal"
            header-text-variant="light"
            header-bg-variant="primary"
            hide-footer
            hide-backdrop
            hide-header-close
            title="실험 결과"
            >
            <div id="test_result_user">
                실험자 :: {{ this.open_result_user }}
            </div>
            <div id="open_result_icon_area">
                <img class="open_result_icon" v-if="this.open_result == 'red_1'" src="@/assets/img/game_icon/red_+.png">
                <img class="open_result_icon" v-else-if="this.open_result == 'red_0'" src="@/assets/img/game_icon/red_-.png">
                <img class="open_result_icon" v-else-if="this.open_result == 'blue_1'" src="@/assets/img/game_icon/blue_+.png">
                <img class="open_result_icon" v-else-if="this.open_result == 'blue_0'" src="@/assets/img/game_icon/blue_-.png">
                <img class="open_result_icon" v-else-if="this.open_result == 'green_1'" src="@/assets/img/game_icon/green_+.png">
                <img class="open_result_icon" v-else-if="this.open_result == 'green_0'" src="@/assets/img/game_icon/green_-.png">
                <img class="open_result_icon" v-else-if="this.open_result == 'blank'" src="@/assets/img/game_icon/blank.png">
            </div>
        </b-modal>

        <!-- 논문 반박/발표 모달 -->
        <b-modal id="presentation_of_theories_modal" 
            ref="presentation_of_theories_modal"
            v-model="presentation_of_theories_modal"
            header-text-variant="light"
            header-bg-variant="primary"
            hide-footer
            hide-backdrop
            hide-header-close
            title="논문 발표"
            >
            <div v-if="this.my_turn == true">
                <div class="modal_in">
                    <p v-if="this.board_5_selected == true "> 논문 반박하기 </p>
                    <p v-else-if="this.board_6_selected == true "> 논문 발표하기 </p>
                    <p>어떤 것을 고르시겠습니까?  
                        <b-button pill id="my_reasoning_table_open" v-on:click="reasoning_table_open">
                        추리 테이블 보기
                        </b-button>
                    </p>
                    
                    <div class="select_area">
                        <b-container>
                            <b-row class="theory_board" v-for="n in 8" :key="n">
                                <b-col cols="3" class="theory_ingre_col">
                                    <img class="theory_ingre_icon" :src="this.icon_data[n-1]" @click="theory_presentation(n)">
                                </b-col>
                                <b-col cols="3" class="theory_ele_col">
                                    <img class="theory_ele_icon" :src="this.reasoning_data[this.theory_data[n].element - 1]">
                                </b-col>
                                <b-col cols="3" class="theory_stamp_col">
                                    <b-row v-for="m in 3" :key="m">
                                        <b-col v-if="this.theory_data[n].stamp[m].user_key == this.my_key">
                                            <img class="theory_stamp" 
                                                 v-if="this.theory_data[n].stamp[m].color == 'red' && (
                                                       this.theory_data[n].stamp[m].point == 'point_5_1' || 
                                                       this.theory_data[n].stamp[m].point == 'point_5_2'
                                                        )"
                                                 src="@/assets/img/stamp/stamp_red_5.png"
                                                >
                                            <img class="theory_stamp" 
                                            v-else-if="this.theory_data[n].stamp[m].color == 'red' && (
                                                       this.theory_data[n].stamp[m].point == 'point_3_1' || 
                                                       this.theory_data[n].stamp[m].point == 'point_3_2' ||
                                                       this.theory_data[n].stamp[m].point == 'point_3_3'
                                                        )"
                                                 src="@/assets/img/stamp/stamp_red_3.png"
                                                >
                                            <img class="theory_stamp" 
                                            v-else-if="this.theory_data[n].stamp[m].color == 'red' && (
                                                       this.theory_data[n].stamp[m].point == 'question_red_1' || 
                                                       this.theory_data[n].stamp[m].point == 'question_red_2'
                                                        )"
                                                 src="@/assets/img/stamp/stamp_red_red.png"
                                                >
                                            <img class="theory_stamp" 
                                            v-else-if="this.theory_data[n].stamp[m].color == 'red' && (
                                                       this.theory_data[n].stamp[m].point == 'question_green_1' || 
                                                       this.theory_data[n].stamp[m].point == 'question_green_2'
                                                        )"
                                                 src="@/assets/img/stamp/stamp_red_green.png"
                                                >
                                            <img class="theory_stamp" 
                                            v-else-if="this.theory_data[n].stamp[m].color == 'red' && (
                                                       this.theory_data[n].stamp[m].point == 'question_blue_1' || 
                                                       this.theory_data[n].stamp[m].point == 'question_blue_2'
                                                        )"
                                                 src="@/assets/img/stamp/stamp_red_blue.png"
                                                >
                                            <img class="theory_stamp" 
                                            v-else-if="this.theory_data[n].stamp[m].color == 'blue' && (
                                                       this.theory_data[n].stamp[m].point == 'point_5_1' || 
                                                       this.theory_data[n].stamp[m].point == 'point_5_2'
                                                        )"
                                                 src="@/assets/img/stamp/stamp_blue_5.png"
                                                >
                                            <img class="theory_stamp" 
                                            v-else-if="this.theory_data[n].stamp[m].color == 'blue' && (
                                                       this.theory_data[n].stamp[m].point == 'point_3_1' || 
                                                       this.theory_data[n].stamp[m].point == 'point_3_2' ||
                                                       this.theory_data[n].stamp[m].point == 'point_3_3'
                                                        )"
                                                 src="@/assets/img/stamp/stamp_blue_3.png"
                                                >
                                            <img class="theory_stamp" 
                                            v-else-if="this.theory_data[n].stamp[m].color == 'blue' && (
                                                       this.theory_data[n].stamp[m].point == 'question_red_1' || 
                                                       this.theory_data[n].stamp[m].point == 'question_red_2'
                                                        )"
                                                 src="@/assets/img/stamp/stamp_blue_red.png"
                                                >
                                            <img class="theory_stamp" 
                                            v-else-if="this.theory_data[n].stamp[m].color == 'blue' && (
                                                       this.theory_data[n].stamp[m].point == 'question_green_1' || 
                                                       this.theory_data[n].stamp[m].point == 'question_green_2'
                                                        )"
                                                 src="@/assets/img/stamp/stamp_blue_green.png"
                                                >
                                            <img class="theory_stamp" 
                                            v-else-if="this.theory_data[n].stamp[m].color == 'blue' && (
                                                       this.theory_data[n].stamp[m].point == 'question_blue_1' || 
                                                       this.theory_data[n].stamp[m].point == 'question_blue_2'
                                                        )"
                                                 src="@/assets/img/stamp/stamp_blue_blue.png"
                                                >
                                            <img class="theory_stamp" 
                                            v-else-if="this.theory_data[n].stamp[m].color == 'black' && (
                                                       this.theory_data[n].stamp[m].point == 'point_5_1' || 
                                                       this.theory_data[n].stamp[m].point == 'point_5_2'
                                                        )"
                                                 src="@/assets/img/stamp/stamp_black_5.png"
                                                >
                                            <img class="theory_stamp" 
                                            v-else-if="this.theory_data[n].stamp[m].color == 'black' && (
                                                       this.theory_data[n].stamp[m].point == 'point_3_1' || 
                                                       this.theory_data[n].stamp[m].point == 'point_3_2' || 
                                                       this.theory_data[n].stamp[m].point == 'point_3_3'
                                                        )"
                                                 src="@/assets/img/stamp/stamp_black_3.png"
                                                >
                                            <img class="theory_stamp" 
                                            v-else-if="this.theory_data[n].stamp[m].color == 'black' && (
                                                       this.theory_data[n].stamp[m].point == 'question_red_1' || 
                                                       this.theory_data[n].stamp[m].point == 'question_red_2'
                                                        )"
                                                 src="@/assets/img/stamp/stamp_black_red.png"
                                                >
                                            <img class="theory_stamp" 
                                            v-else-if="this.theory_data[n].stamp[m].color == 'black' && (
                                                       this.theory_data[n].stamp[m].point == 'question_green_1' || 
                                                       this.theory_data[n].stamp[m].point == 'question_green_2'
                                                        )"
                                                 src="@/assets/img/stamp/stamp_black_green.png"
                                                >
                                            <img class="theory_stamp" 
                                            v-else-if="this.theory_data[n].stamp[m].color == 'black' && (
                                                       this.theory_data[n].stamp[m].point == 'question_blue_1' || 
                                                       this.theory_data[n].stamp[m].point == 'question_blue_2'
                                                        )"
                                                 src="@/assets/img/stamp/stamp_black_blue.png"
                                                >
                                            <img class="theory_stamp" 
                                            v-else-if="this.theory_data[n].stamp[m].color == 'white' && (
                                                       this.theory_data[n].stamp[m].point == 'point_5_1' || 
                                                       this.theory_data[n].stamp[m].point == 'point_5_2'
                                                        )"
                                                 src="@/assets/img/stamp/stamp_white_5.png"
                                                >
                                            <img class="theory_stamp" 
                                            v-else-if="this.theory_data[n].stamp[m].color == 'white' && (
                                                       this.theory_data[n].stamp[m].point == 'point_3_1' || 
                                                       this.theory_data[n].stamp[m].point == 'point_3_2' ||
                                                       this.theory_data[n].stamp[m].point == 'point_3_3'
                                                        )"
                                                 src="@/assets/img/stamp/stamp_white_3.png"
                                                >
                                            <img class="theory_stamp" 
                                            v-else-if="this.theory_data[n].stamp[m].color == 'white' && (
                                                       this.theory_data[n].stamp[m].point == 'question_red_1' || 
                                                       this.theory_data[n].stamp[m].point == 'question_red_2'
                                                        )"
                                                 src="@/assets/img/stamp/stamp_white_red.png"
                                                >
                                            <img class="theory_stamp" 
                                            v-else-if="this.theory_data[n].stamp[m].color == 'white' && (
                                                       this.theory_data[n].stamp[m].point == 'question_green_1' || 
                                                       this.theory_data[n].stamp[m].point == 'question_green_2'
                                                        )"
                                                 src="@/assets/img/stamp/stamp_white_green.png"
                                                >
                                            <img class="theory_stamp" 
                                            v-else-if="this.theory_data[n].stamp[m].color == 'white' && (
                                                       this.theory_data[n].stamp[m].point == 'question_blue_1' || 
                                                       this.theory_data[n].stamp[m].point == 'question_blue_2'
                                                        )"
                                                 src="@/assets/img/stamp/stamp_white_blue.png"
                                                >
                                        </b-col>
                                        <b-col v-else-if="this.theory_data[n].stamp[m].user_key != this.my_key && 
                                                        this.theory_data[n].stamp[m].color != '' ">
                                            <img class="theory_stamp" 
                                                v-if="this.theory_data[n].stamp[m].color == 'red'"
                                                src="@/assets/img/stamp/stamp_red_back.png"
                                                >
                                            <img class="theory_stamp" 
                                                v-else-if="this.theory_data[n].stamp[m].color == 'blue'"
                                                src="@/assets/img/stamp/stamp_blue_back.png"
                                                >
                                            <img class="theory_stamp" 
                                                v-else-if="this.theory_data[n].stamp[m].color == 'black'"
                                                src="@/assets/img/stamp/stamp_black_back.png"
                                                >
                                            <img class="theory_stamp" 
                                                v-else-if="this.theory_data[n].stamp[m].color == 'white'"
                                                src="@/assets/img/stamp/stamp_white_back.png"
                                                >
                                        </b-col>
                                        <b-col v-else>
                                            
                                        </b-col>
                                    </b-row>
                                </b-col>
                            </b-row>
                        </b-container>
                    </div>
                </div>
            </div>
            <div v-else>
                <div class="modal_out">
                    <div class="not_my_turn">
                        <b-spinner label="Spinning"></b-spinner>
                        <p>
                            다른 사람이 발표 중 입니다. 잠시 기다려주세요
                        </p>
                        <p>
                            <b-button pill id="my_reasoning_table_open" v-on:click="reasoning_table_open">
                                추리 테이블 보기
                            </b-button>
                        </p>
                    </div>
                </div>
            </div>
        </b-modal>

        <!-- 논문 발표 재료의 원소 선택 모달 -->
        <b-modal id="ingre_presentation_modal" 
            centered 
            ref="ingre_presentation_modal"
            v-model="ingre_presentation_modal_onoff"
            header-text-variant="light"
            header-bg-variant="primary"
            hide-footer
            hide-backdrop
            hide-header-close
            title="주장하기"
            >
            <div v-if="this.board_6_selected == true" id="ingre_presentation_modal_intitle">
                <p> 선택된 재료
                    <img class="pre_modal_select_ingre_img" v-if="this.now_select_ingre > 0 " :src="this.icon_data[this.now_select_ingre-1]">
                </p>
                <br>
                <p>
                    선택한 재료의 원소를 골라주세요
                </p>
                <p>
                    <b-container id="ingre_pre_modal_ele_select_area">
                        <b-row>
                            <b-col class="ingre_pre_ele" v-for="n in 8" :key="n">
                                <img class="ingre_pre_ele_icon" :src="this.reasoning_data[n-1]" v-bind:ref="'ele_'+n" @click="theory_element_select(n)">
                            </b-col>
                        </b-row>
                    </b-container>
                </p>
                <p>
                    선택할 스탬프를 골라주세요
                </p>
                <p>
                    <b-container id="ingre_pre_modal_stamp_select_area">
                        <b-row>
                            <b-col class="ingre_pre_stamp" v-if="this.my_color == 'red'" >
                                <img class="ingre_pre_ele_icon" v-if="this.my_data.user_ingame_data.have_stamp.point_5_1" :src="this.red_stamp_data[0]" ref="stm_1" @click="theory_stamp_select(1)">
                                <img class="ingre_pre_ele_icon" v-if="this.my_data.user_ingame_data.have_stamp.point_5_2" :src="this.red_stamp_data[1]" ref="stm_2" @click="theory_stamp_select(2)">
                                <img class="ingre_pre_ele_icon" v-if="this.my_data.user_ingame_data.have_stamp.point_3_1" :src="this.red_stamp_data[2]" ref="stm_3" @click="theory_stamp_select(3)">
                                <img class="ingre_pre_ele_icon" v-if="this.my_data.user_ingame_data.have_stamp.point_3_2" :src="this.red_stamp_data[3]" ref="stm_4" @click="theory_stamp_select(4)">
                                <img class="ingre_pre_ele_icon" v-if="this.my_data.user_ingame_data.have_stamp.point_3_3" :src="this.red_stamp_data[4]" ref="stm_5" @click="theory_stamp_select(5)">
                                <img class="ingre_pre_ele_icon" v-if="this.my_data.user_ingame_data.have_stamp.question_red_1" :src="this.red_stamp_data[5]" ref="stm_6" @click="theory_stamp_select(6)">
                                <img class="ingre_pre_ele_icon" v-if="this.my_data.user_ingame_data.have_stamp.question_red_2" :src="this.red_stamp_data[6]" ref="stm_7" @click="theory_stamp_select(7)">
                                <img class="ingre_pre_ele_icon" v-if="this.my_data.user_ingame_data.have_stamp.question_green_1" :src="this.red_stamp_data[7]" ref="stm_8" @click="theory_stamp_select(8)">
                                <img class="ingre_pre_ele_icon" v-if="this.my_data.user_ingame_data.have_stamp.question_green_2" :src="this.red_stamp_data[8]" ref="stm_9" @click="theory_stamp_select(9)">
                                <img class="ingre_pre_ele_icon" v-if="this.my_data.user_ingame_data.have_stamp.question_blue_1" :src="this.red_stamp_data[9]" ref="stm_10" @click="theory_stamp_select(10)">
                                <img class="ingre_pre_ele_icon" v-if="this.my_data.user_ingame_data.have_stamp.question_blue_2" :src="this.red_stamp_data[10]" ref="stm_11" @click="theory_stamp_select(11)">
                            </b-col>
                            <b-col class="ingre_pre_stamp" v-else-if="this.my_color == 'blue'" >
                                <img class="ingre_pre_ele_icon" v-if="this.my_data.user_ingame_data.have_stamp.point_5_1" :src="this.blue_stamp_data[0]" ref="stm_1" @click="theory_stamp_select(1)">
                                <img class="ingre_pre_ele_icon" v-if="this.my_data.user_ingame_data.have_stamp.point_5_2" :src="this.blue_stamp_data[1]" ref="stm_2" @click="theory_stamp_select(2)">
                                <img class="ingre_pre_ele_icon" v-if="this.my_data.user_ingame_data.have_stamp.point_3_1" :src="this.blue_stamp_data[2]" ref="stm_3" @click="theory_stamp_select(3)">
                                <img class="ingre_pre_ele_icon" v-if="this.my_data.user_ingame_data.have_stamp.point_3_2" :src="this.blue_stamp_data[3]" ref="stm_4" @click="theory_stamp_select(4)">
                                <img class="ingre_pre_ele_icon" v-if="this.my_data.user_ingame_data.have_stamp.point_3_3" :src="this.blue_stamp_data[4]" ref="stm_5" @click="theory_stamp_select(5)">
                                <img class="ingre_pre_ele_icon" v-if="this.my_data.user_ingame_data.have_stamp.question_red_1" :src="this.blue_stamp_data[5]" ref="stm_6" @click="theory_stamp_select(6)">
                                <img class="ingre_pre_ele_icon" v-if="this.my_data.user_ingame_data.have_stamp.question_red_2" :src="this.blue_stamp_data[6]" ref="stm_7" @click="theory_stamp_select(7)">
                                <img class="ingre_pre_ele_icon" v-if="this.my_data.user_ingame_data.have_stamp.question_green_1" :src="this.blue_stamp_data[7]" ref="stm_8" @click="theory_stamp_select(8)">
                                <img class="ingre_pre_ele_icon" v-if="this.my_data.user_ingame_data.have_stamp.question_green_2" :src="this.blue_stamp_data[8]" ref="stm_9" @click="theory_stamp_select(9)">
                                <img class="ingre_pre_ele_icon" v-if="this.my_data.user_ingame_data.have_stamp.question_blue_1" :src="this.blue_stamp_data[9]" ref="stm_10" @click="theory_stamp_select(10)">
                                <img class="ingre_pre_ele_icon" v-if="this.my_data.user_ingame_data.have_stamp.question_blue_2" :src="this.blue_stamp_data[10]" ref="stm_11" @click="theory_stamp_select(11)">
                            </b-col>
                            <b-col class="ingre_pre_stamp" v-else-if="this.my_color == 'black'" >
                                <img class="ingre_pre_ele_icon" v-if="this.my_data.user_ingame_data.have_stamp.point_5_1" :src="this.black_stamp_data[0]" ref="stm_1" @click="theory_stamp_select(1)">
                                <img class="ingre_pre_ele_icon" v-if="this.my_data.user_ingame_data.have_stamp.point_5_2" :src="this.black_stamp_data[1]" ref="stm_2" @click="theory_stamp_select(2)">
                                <img class="ingre_pre_ele_icon" v-if="this.my_data.user_ingame_data.have_stamp.point_3_1" :src="this.black_stamp_data[2]" ref="stm_3" @click="theory_stamp_select(3)">
                                <img class="ingre_pre_ele_icon" v-if="this.my_data.user_ingame_data.have_stamp.point_3_2" :src="this.black_stamp_data[3]" ref="stm_4" @click="theory_stamp_select(4)">
                                <img class="ingre_pre_ele_icon" v-if="this.my_data.user_ingame_data.have_stamp.point_3_3" :src="this.black_stamp_data[4]" ref="stm_5" @click="theory_stamp_select(5)">
                                <img class="ingre_pre_ele_icon" v-if="this.my_data.user_ingame_data.have_stamp.question_red_1" :src="this.black_stamp_data[5]" ref="stm_6" @click="theory_stamp_select(6)">
                                <img class="ingre_pre_ele_icon" v-if="this.my_data.user_ingame_data.have_stamp.question_red_2" :src="this.black_stamp_data[6]" ref="stm_7" @click="theory_stamp_select(7)">
                                <img class="ingre_pre_ele_icon" v-if="this.my_data.user_ingame_data.have_stamp.question_green_1" :src="this.black_stamp_data[7]" ref="stm_8" @click="theory_stamp_select(8)">
                                <img class="ingre_pre_ele_icon" v-if="this.my_data.user_ingame_data.have_stamp.question_green_2" :src="this.black_stamp_data[8]" ref="stm_9" @click="theory_stamp_select(9)">
                                <img class="ingre_pre_ele_icon" v-if="this.my_data.user_ingame_data.have_stamp.question_blue_1" :src="this.black_stamp_data[9]" ref="stm_10" @click="theory_stamp_select(10)">
                                <img class="ingre_pre_ele_icon" v-if="this.my_data.user_ingame_data.have_stamp.question_blue_2" :src="this.black_stamp_data[10]" ref="stm_11" @click="theory_stamp_select(11)">
                            </b-col>
                            <b-col class="ingre_pre_stamp" v-else-if="this.my_color == 'white'" >
                                <img class="ingre_pre_ele_icon" v-if="this.my_data.user_ingame_data.have_stamp.point_5_1" :src="this.white_stamp_data[0]" ref="stm_1" @click="theory_stamp_select(1)">
                                <img class="ingre_pre_ele_icon" v-if="this.my_data.user_ingame_data.have_stamp.point_5_2" :src="this.white_stamp_data[1]" ref="stm_2" @click="theory_stamp_select(2)">
                                <img class="ingre_pre_ele_icon" v-if="this.my_data.user_ingame_data.have_stamp.point_3_1" :src="this.white_stamp_data[2]" ref="stm_3" @click="theory_stamp_select(3)">
                                <img class="ingre_pre_ele_icon" v-if="this.my_data.user_ingame_data.have_stamp.point_3_2" :src="this.white_stamp_data[3]" ref="stm_4" @click="theory_stamp_select(4)">
                                <img class="ingre_pre_ele_icon" v-if="this.my_data.user_ingame_data.have_stamp.point_3_3" :src="this.white_stamp_data[4]" ref="stm_5" @click="theory_stamp_select(5)">
                                <img class="ingre_pre_ele_icon" v-if="this.my_data.user_ingame_data.have_stamp.question_red_1" :src="this.white_stamp_data[5]" ref="stm_6" @click="theory_stamp_select(6)">
                                <img class="ingre_pre_ele_icon" v-if="this.my_data.user_ingame_data.have_stamp.question_red_2" :src="this.white_stamp_data[6]" ref="stm_7" @click="theory_stamp_select(7)">
                                <img class="ingre_pre_ele_icon" v-if="this.my_data.user_ingame_data.have_stamp.question_green_1" :src="this.white_stamp_data[7]" ref="stm_8" @click="theory_stamp_select(8)">
                                <img class="ingre_pre_ele_icon" v-if="this.my_data.user_ingame_data.have_stamp.question_green_2" :src="this.white_stamp_data[8]" ref="stm_9" @click="theory_stamp_select(9)">
                                <img class="ingre_pre_ele_icon" v-if="this.my_data.user_ingame_data.have_stamp.question_blue_1" :src="this.white_stamp_data[9]" ref="stm_10" @click="theory_stamp_select(10)">
                                <img class="ingre_pre_ele_icon" v-if="this.my_data.user_ingame_data.have_stamp.question_blue_2" :src="this.white_stamp_data[10]" ref="stm_11" @click="theory_stamp_select(11)">
                            </b-col>
                        </b-row>
                    </b-container>
                </p>
            </div>

            <div v-else-if="this.board_5_selected == true" id="ingre_refuting_modal_intitle">
                <p> 선택된 재료
                    <span ref="select_ingre" class="pre_modal_select_ingre_span"></span>
                </p>
                <p> 해당 재료에 주장된 원소
                    <span ref="now_presen" class="pre_modal_select_ele_span">
                        <img class="pre_modal_select_ele_img" v-if="this.theory_data[this.now_select_ingre].element == 1"      :src="this.reasoning_data[0]">
                        <img class="pre_modal_select_ele_img" v-else-if="this.theory_data[this.now_select_ingre].element == 2" :src="this.reasoning_data[1]">
                        <img class="pre_modal_select_ele_img" v-else-if="this.theory_data[this.now_select_ingre].element == 3" :src="this.reasoning_data[2]">
                        <img class="pre_modal_select_ele_img" v-else-if="this.theory_data[this.now_select_ingre].element == 4" :src="this.reasoning_data[3]">
                        <img class="pre_modal_select_ele_img" v-else-if="this.theory_data[this.now_select_ingre].element == 5" :src="this.reasoning_data[4]">
                        <img class="pre_modal_select_ele_img" v-else-if="this.theory_data[this.now_select_ingre].element == 6" :src="this.reasoning_data[5]">
                        <img class="pre_modal_select_ele_img" v-else-if="this.theory_data[this.now_select_ingre].element == 7" :src="this.reasoning_data[6]">
                        <img class="pre_modal_select_ele_img" v-else-if="this.theory_data[this.now_select_ingre].element == 8" :src="this.reasoning_data[7]">
                    </span>
                </p>
                <br>
                <p>
                    반박할 원소를 골라주세요
                </p>
                <p>
                    <b-container id="ingre_pre_modal_ele_select_area">
                        <b-row>
                            <b-col class="ingre_pre_ele" v-for="n in 3" :key="n">
                                <img class="ingre_pre_ele_icon" :src="this.ele_core_data[n-1]" :ref="'ele_core_'+n" @click="theory_origin_select(n)">
                            </b-col>
                        </b-row>
                    </b-container>
                </p>
                <p>
                    반박을 증명할 재료 2개를 골라주세요
                </p>
                <p>
                    <b-container id="ingre_pre_modal_ingre_select_area">
                        <b-row>
                            <b-col class="ingre_pre_ingre" v-for="n in 8" :key="n">
                                <img class="ingre_pre_ingre_icon" :src="this.icon_data[n-1]" :ref="'theory_ingre_'+n" @click="theory_ingre_select(n)">
                            </b-col>
                        </b-row>
                    </b-container>
                </p>
            </div>
            <div id="decide_ingre_pre_select_btn_area">
                <b-button variant="outline-success" block class="reasoning_table_button" @click="theory_confirm()"
                    v-if="this.board_5_selected == true"
                    >
                    반박하기
                </b-button>
                <b-button variant="outline-success" block class="reasoning_table_button" @click="theory_confirm()"
                    v-else-if="this.board_6_selected == true"
                    >
                    발표하기
                </b-button>
                <b-button variant="outline-danger" class="reasoning_table_button" data-bs-dismiss="modal">취소</b-button>
            </div> 
        </b-modal>

        <!-- 반박 성공 확인 모달 -->
        <b-modal id="success_refute_modal_onoff" 
            centered 
            ref="success_refute_modal_onoff"
            v-model="success_refute_modal_onoff"
            header-text-variant="light"
            header-bg-variant="primary"
            hide-footer
            hide-backdrop
            hide-header-close
            title="확인창"
            >
            <div v-if="this.reciving_data.success == 'true' || this.reciving_data.success == true" id="success_refute_modal_intitle">
                <p>반박된 원소와 색</p>
                <p>
                    <img class="success_refute_icon" v-if="this.reciving_data.ingre_num == '1'" src="@/assets/img/material_card/ingredient_1.png">
                    <img class="success_refute_icon" v-else-if="this.reciving_data.ingre_num == '2'" src="@/assets/img/material_card/ingredient_2.png">
                    <img class="success_refute_icon" v-else-if="this.reciving_data.ingre_num == '3'" src="@/assets/img/material_card/ingredient_3.png">
                    <img class="success_refute_icon" v-else-if="this.reciving_data.ingre_num == '4'" src="@/assets/img/material_card/ingredient_4.png">
                    <img class="success_refute_icon" v-else-if="this.reciving_data.ingre_num == '5'" src="@/assets/img/material_card/ingredient_5.png">
                    <img class="success_refute_icon" v-else-if="this.reciving_data.ingre_num == '6'" src="@/assets/img/material_card/ingredient_6.png">
                    <img class="success_refute_icon" v-else-if="this.reciving_data.ingre_num == '7'" src="@/assets/img/material_card/ingredient_7.png">
                    <img class="success_refute_icon" v-else-if="this.reciving_data.ingre_num == '8'" src="@/assets/img/material_card/ingredient_8.png">

                    <img class="success_refute_ori" v-if="this.reciving_data.ori == '1' " src="@/assets/img/game_icon/red_origin.png" >
                    <img class="success_refute_ori" v-else-if="this.reciving_data.ori == '2' " src="@/assets/img/game_icon/green_origin.png" >
                    <img class="success_refute_ori" v-else-if="this.reciving_data.ori == '3' " src="@/assets/img/game_icon/blue_origin.png" >
                </p>
                <p>
                    반박에 성공하여 해당 인장의 주인이 감점처리 됩니다!
                </p>
                <p>
                    단, 반박된 색과 같은 색의 인장의 주인은 감점처리되지 않습니다
                </p>
                <b-container id="refute_value_area">
                    <b-row>
                        <b-col v-for="n in this.reciving_data.stamp" :key="n">
                            <img class="refute_stamp_list" v-if="n.color == 'red' && n.point == 'point_5_1'"      src="@/assets/img/stamp/stamp_red_5.png" >
                            <img class="refute_stamp_list" v-else-if="n.color == 'red' && n.point == 'point_5_2'" src="@/assets/img/stamp/stamp_red_5.png" >
                            <img class="refute_stamp_list" v-else-if="n.color == 'red' && n.point == 'point_3_1'" src="@/assets/img/stamp/stamp_red_3.png" >
                            <img class="refute_stamp_list" v-else-if="n.color == 'red' && n.point == 'point_3_2'" src="@/assets/img/stamp/stamp_red_3.png" >
                            <img class="refute_stamp_list" v-else-if="n.color == 'red' && n.point == 'point_3_3'" src="@/assets/img/stamp/stamp_red_3.png" >
                            <img class="refute_stamp_list" v-else-if="n.color == 'red' && n.point == 'question_red_1'" src="@/assets/img/stamp/stamp_red_red.png" >
                            <img class="refute_stamp_list" v-else-if="n.color == 'red' && n.point == 'question_red_2'" src="@/assets/img/stamp/stamp_red_red.png" >
                            <img class="refute_stamp_list" v-else-if="n.color == 'red' && n.point == 'question_blue_1'" src="@/assets/img/stamp/stamp_red_blue.png" >
                            <img class="refute_stamp_list" v-else-if="n.color == 'red' && n.point == 'question_blue_2'" src="@/assets/img/stamp/stamp_red_blue.png" >
                            <img class="refute_stamp_list" v-else-if="n.color == 'red' && n.point == 'question_green_1'" src="@/assets/img/stamp/stamp_red_green.png" >
                            <img class="refute_stamp_list" v-else-if="n.color == 'red' && n.point == 'question_green_2'" src="@/assets/img/stamp/stamp_red_green.png" >
                            <img class="refute_stamp_list" v-else-if="n.color == 'blue' && n.point == 'point_5_1'" src="@/assets/img/stamp/stamp_blue_5.png" >
                            <img class="refute_stamp_list" v-else-if="n.color == 'blue' && n.point == 'point_5_2'" src="@/assets/img/stamp/stamp_blue_5.png" >
                            <img class="refute_stamp_list" v-else-if="n.color == 'blue' && n.point == 'point_3_1'" src="@/assets/img/stamp/stamp_blue_3.png" >
                            <img class="refute_stamp_list" v-else-if="n.color == 'blue' && n.point == 'point_3_2'" src="@/assets/img/stamp/stamp_blue_3.png" >
                            <img class="refute_stamp_list" v-else-if="n.color == 'blue' && n.point == 'point_3_3'" src="@/assets/img/stamp/stamp_blue_3.png" >
                            <img class="refute_stamp_list" v-else-if="n.color == 'blue' && n.point == 'question_red_1'" src="@/assets/img/stamp/stamp_blue_red.png" >
                            <img class="refute_stamp_list" v-else-if="n.color == 'blue' && n.point == 'question_red_2'" src="@/assets/img/stamp/stamp_blue_red.png" >
                            <img class="refute_stamp_list" v-else-if="n.color == 'blue' && n.point == 'question_blue_1'" src="@/assets/img/stamp/stamp_blue_blue.png" >
                            <img class="refute_stamp_list" v-else-if="n.color == 'blue' && n.point == 'question_blue_2'" src="@/assets/img/stamp/stamp_blue_blue.png" >
                            <img class="refute_stamp_list" v-else-if="n.color == 'blue' && n.point == 'question_green_1'" src="@/assets/img/stamp/stamp_blue_green.png" >
                            <img class="refute_stamp_list" v-else-if="n.color == 'blue' && n.point == 'question_green_2'" src="@/assets/img/stamp/stamp_blue_green.png" >
                            <img class="refute_stamp_list" v-else-if="n.color == 'white' && n.point == 'point_5_1'" src="@/assets/img/stamp/stamp_white_5.png" >
                            <img class="refute_stamp_list" v-else-if="n.color == 'white' && n.point == 'point_5_2'" src="@/assets/img/stamp/stamp_white_5.png" >
                            <img class="refute_stamp_list" v-else-if="n.color == 'white' && n.point == 'point_3_1'" src="@/assets/img/stamp/stamp_white_3.png" >
                            <img class="refute_stamp_list" v-else-if="n.color == 'white' && n.point == 'point_3_2'" src="@/assets/img/stamp/stamp_white_3.png" >
                            <img class="refute_stamp_list" v-else-if="n.color == 'white' && n.point == 'point_3_3'" src="@/assets/img/stamp/stamp_white_3.png" >
                            <img class="refute_stamp_list" v-else-if="n.color == 'white' && n.point == 'question_red_1'" src="@/assets/img/stamp/stamp_white_red.png" >
                            <img class="refute_stamp_list" v-else-if="n.color == 'white' && n.point == 'question_red_2'" src="@/assets/img/stamp/stamp_white_red.png" >
                            <img class="refute_stamp_list" v-else-if="n.color == 'white' && n.point == 'question_blue_1'" src="@/assets/img/stamp/stamp_white_blue.png" >
                            <img class="refute_stamp_list" v-else-if="n.color == 'white' && n.point == 'question_blue_2'" src="@/assets/img/stamp/stamp_white_blue.png" >
                            <img class="refute_stamp_list" v-else-if="n.color == 'white' && n.point == 'question_green_1'" src="@/assets/img/stamp/stamp_white_green.png" >
                            <img class="refute_stamp_list" v-else-if="n.color == 'white' && n.point == 'question_green_2'" src="@/assets/img/stamp/stamp_white_green.png" >
                            <img class="refute_stamp_list" v-else-if="n.color == 'black' && n.point == 'point_5_1'" src="@/assets/img/stamp/stamp_black_5.png" >
                            <img class="refute_stamp_list" v-else-if="n.color == 'black' && n.point == 'point_5_2'" src="@/assets/img/stamp/stamp_black_5.png" >
                            <img class="refute_stamp_list" v-else-if="n.color == 'black' && n.point == 'point_3_1'" src="@/assets/img/stamp/stamp_black_3.png" >
                            <img class="refute_stamp_list" v-else-if="n.color == 'black' && n.point == 'point_3_2'" src="@/assets/img/stamp/stamp_black_3.png" >
                            <img class="refute_stamp_list" v-else-if="n.color == 'black' && n.point == 'point_3_3'" src="@/assets/img/stamp/stamp_black_3.png" >
                            <img class="refute_stamp_list" v-else-if="n.color == 'black' && n.point == 'question_red_1'" src="@/assets/img/stamp/stamp_black_red.png" >
                            <img class="refute_stamp_list" v-else-if="n.color == 'black' && n.point == 'question_red_2'" src="@/assets/img/stamp/stamp_black_red.png" >
                            <img class="refute_stamp_list" v-else-if="n.color == 'black' && n.point == 'question_blue_1'" src="@/assets/img/stamp/stamp_black_blue.png" >
                            <img class="refute_stamp_list" v-else-if="n.color == 'black' && n.point == 'question_blue_2'" src="@/assets/img/stamp/stamp_black_blue.png" >
                            <img class="refute_stamp_list" v-else-if="n.color == 'black' && n.point == 'question_green_1'" src="@/assets/img/stamp/stamp_black_green.png" >
                            <img class="refute_stamp_list" v-else-if="n.color == 'black' && n.point == 'question_green_2'" src="@/assets/img/stamp/stamp_black_green.png" >
                        </b-col>
                    </b-row>
                </b-container>
                <br>
                <p>
                    위의 인장들은 다시 사용할 수 없습니다!
                </p>
            </div>

            <div v-if="this.reciving_data.success == 'false' || this.reciving_data.success == false" id="fail_refute_modal_intitle">
                <div class="fail_refute_content">
                    반박 실패
                </div> 
            </div>

            <div id="decide_reasoning_select_btn_area">
                <b-button variant="outline-success" block class="reasoning_table_button" 
                    @click="check_refute_info()"
                    >
                    확인했습니다
                </b-button>
            </div> 
        </b-modal>

        <!-- 물약 시연 모달 -->
        <b-modal id="last_round_modal" 
            centered 
            size="lg"
            ref="last_round_modal"
            v-model="last_round_modal_onoff"
            header-text-variant="light"
            header-bg-variant="primary"
            body-bg-variant="secondary"
            hide-footer
            hide-header-close
            hide-backdrop
            no-close-on-backdrop
            title="물약 전시회"
            >
            <div>
                <div class="exhibition_modal_main">
                    <p>전시할 물약을 선택해주세요</p>
                    <div class="exhibition_select_area">
                        <b-container>
                            <b-row>
                                <b-col class="potion_depolyment">
                                    <img class="potion_depolyment_icon" src="@/assets/img/game_icon/red_+.png"   @click="click_exhibition_potion(1)">
                                </b-col>
                                <b-col class="potion_depolyment">
                                    <img class="potion_depolyment_icon" src="@/assets/img/game_icon/red_-.png"   @click="click_exhibition_potion(2)">
                                </b-col>
                                <b-col class="potion_depolyment">
                                    <img class="potion_depolyment_icon" src="@/assets/img/game_icon/green_+.png" @click="click_exhibition_potion(3)">
                                </b-col>
                                <b-col class="potion_depolyment">
                                    <img class="potion_depolyment_icon" src="@/assets/img/game_icon/green_-.png" @click="click_exhibition_potion(4)">
                                </b-col>
                                <b-col class="potion_depolyment">
                                    <img class="potion_depolyment_icon" src="@/assets/img/game_icon/blue_+.png"  @click="click_exhibition_potion(5)">
                                </b-col>
                                <b-col class="potion_depolyment">
                                    <img class="potion_depolyment_icon" src="@/assets/img/game_icon/blue_-.png"  @click="click_exhibition_potion(6)">
                                </b-col>
                            </b-row>
                            <b-row>
                                <b-col v-for="data in this.exhibition_result.first" :key="data">
                                    <img class="exhibition_cube_icon" v-if="data.user_key != '' && data.color == 'red'" src="@/assets/img/game_icon/cube_red.png" >
                                    <img class="exhibition_cube_icon" v-else-if="data.user_key != '' && data.color == 'blue'" src="@/assets/img/game_icon/cube_blue.png" >
                                    <img class="exhibition_cube_icon" v-else-if="data.user_key != '' && data.color == 'white'" src="@/assets/img/game_icon/cube_white.png" >
                                    <img class="exhibition_cube_icon" v-else-if="data.user_key != '' && data.color == 'black'" src="@/assets/img/game_icon/cube_black.png" >
                                </b-col>
                            </b-row>
                            <b-row>
                                <b-col v-for="data in this.exhibition_result.second" :key="data">
                                    <img class="exhibition_cube_icon" v-if="data.user_key != '' && data.color == 'red'" src="@/assets/img/game_icon/cube_red.png" >
                                    <img class="exhibition_cube_icon" v-else-if="data.user_key != '' && data.color == 'blue'" src="@/assets/img/game_icon/cube_blue.png" >
                                    <img class="exhibition_cube_icon" v-else-if="data.user_key != '' && data.color == 'white'" src="@/assets/img/game_icon/cube_white.png" >
                                    <img class="exhibition_cube_icon" v-else-if="data.user_key != '' && data.color == 'black'" src="@/assets/img/game_icon/cube_black.png" >
                                </b-col>
                            </b-row>
                        </b-container>
                    </div>
                    <br>
                </div>
                <b-button pill id="my_reasoning_table_open" v-on:click="reasoning_table_open">
                    추리 테이블 보기
                </b-button>
                <div>
                    <p id="exhibition_announce">
                        먼저 발표한 물약에 대해서만 명성점수 1점을 획득할 수 있습니다. <br>
                        물약 발표를 실패할 경우 명성이 1점 감소됩니다.<br>
                        신중히 결정해주세요<br>
                        해당 보드에 큐브를 놓지 않으셨다면 전시에 참가하실 수 없습니다.
                    </p>
                    <div id="sell_ingredient_btn_area">
                        <b-button variant="outline-success" class="reasoning_table_button" v-on:click="click_sell_ingredient">
                            결정
                        </b-button>
                        <b-button variant="outline-danger" class="reasoning_table_button" data-bs-dismiss="modal">취소</b-button>
                    </div>
                </div> 
            </div>
        </b-modal>

        <!-- 선택한 물약 발표하기 -->
        <b-modal id="exhibition_check_modal" 
            centered 
            size="lg"
            ref="exhibition_check_modal"
            v-model="exhibition_check_modal_onoff"
            header-text-variant="light"
            header-bg-variant="primary"
            hide-footer
            hide-header-close
            hide-backdrop
            no-close-on-backdrop
            title="물약 발표하기"
            >
            <div>
                <div class="exhibit_modal_main">
                    <p class="exhibit_potion">전시할 물약</p>
                    <div class="exhibit_ingre_select_area">
                        <Show_ingredient_card
                            :my_key="this.my_key"
                            :my_data="this.my_data"
                            @test_ingredient_arr="test_ingredient_arr_getter"
                        ></Show_ingredient_card>
                    </div>
                    <br>
                </div>
                <b-button pill id="my_reasoning_table_open" v-on:click="reasoning_table_open">
                    추리 테이블 보기
                </b-button>
                <div>
                    <p id="exhibition_announce">
                        선택한 물약만 증명하면 됩니다. <br>
                        다른 원소는 무시하고 해당 원소가 나올 재료들만 선택하시면 됩니다.<br>
                        증명에 실패하면 명성 1점이 감점됩니다. <br>
                        신중히 결정해주세요
                    </p>
                    <br>
                    <div id="sell_ingredient_btn_area">
                        <b-button variant="outline-success" class="exhibit_btn" @click="click_exhibit_ingre">
                            결정
                        </b-button>
                        <b-button variant="outline-danger" class="exhibit_btn" data-bs-dismiss="modal">취소</b-button>
                    </div>
                </div> 
            </div>
        </b-modal>

        <!-- 물약 전시 결과 announce -->
        <b-modal id="exhibition_result_modal" 
            v-model="exhibition_result_modal"
            header-text-variant="light"
            header-bg-variant="primary"
            hide-footer
            hide-backdrop
            hide-header-close
            title="전시 결과"
            >
            <div id="exhibition_user">
                전시자 :: {{ this.exhibition_result_user }}
            </div>
            <div class="exhibit_result_show_class" v-if="this.exhibit_result == true">
                <div class="exhibit_announce" v-if="this.exhibit_on_cube_success == true">
                    <span class="exhibit_on_cube_success">
                        전시에 성공하여 성공적으로 큐브를 전시했습니다.
                    </span>
                </div>
                <div class="exhibit_announce" v-else>
                    <span class="exhibit_on_cube_fail">
                        조합에는 성공하였으나 늦어서 전시에는 실패하였습니다.
                    </span>
                </div>
            </div>
            <div class="exhibit_result_show_class" v-else-if="this.exhibit_result == false">
                <div class="exhibit_announce">
                    <span id="exhibit_fail_span">
                        전시에 실패하였습니다.
                    </span>
                </div>
            </div>
        </b-modal>

        <!-- 게임 완료 알림 모달 -->
        <b-modal id="game_end_alert_modal" 
            v-model="game_end_alert_modal"
            header-text-variant="light"
            header-bg-variant="success"
            centered
            hide-footer
            hide-backdrop
            hide-header-close
            title="게임 종료"
        >
            <div id="game_end_alert_announce">
                <span>
                    게임이 종료 되었습니다.<br>
                    완료화면으로 이동하세요!
                </span>
            </div>
            <div id="game_end_alert_btn_area">
                <b-button variant="success" class="game_end_alert_confirm_btn" @click="game_end_confirm">
                    완료
                </b-button>
            </div>
        </b-modal>        
        
        <!-- 게임 강제 종료 알림 모달 -->
        <b-modal id="game_forced_termination_alert_modal" 
            v-model="game_forced_termination_alert_modal"
            header-text-variant="light"
            header-bg-variant="danger"
            centered
            hide-footer
            hide-backdrop
            hide-header-close
            title="게임 종료"
        >
            <div id="game_end_alert_announce">
                <span>
                    게임 인원이 줄어 더 이상 진행이 불가능합니다.<br>
                    방 화면으로 강제 이동 됩니다.
                </span>
            </div>
            <div id="game_end_alert_btn_area">
                <b-button variant="success" class="game_end_alert_confirm_btn" @click="forced_termination_confirm">
                    완료
                </b-button>
            </div>
        </b-modal>

        <!-- 나의 추리 테이블 -->
        <b-modal 
            centered
            id="my_reasoning_table"  
            v-model="my_reasoning_table_modal"
            ref="my_reasoning_table"
            header-text-variant="light"
            header-bg-variant="primary"
            hide-footer
            no-close-on-backdrop
            size="xl"
            title="내 추리 테이블"
            >
            <div>
                <div v-for="data in this.result_table" :key="data">
                    <b-container v-if="data.user_key == this.my_key" style="max-width: 100%;">
                        <b-row>
                            <b-col>
                                <b-row>
                                    <b-col>
                                    </b-col>
                                    <b-col class="border-set" v-for="n in 8" :key="n">
                                        <img class="ingredient_icon" :src="this.icon_data[n-1]">
                                    </b-col>
                                </b-row>
                                <b-row v-for="(col, index) in data.ingredient_result" :key="col">
                                    <b-col class="border-set">
                                        <img class="ingredient_icon" :src="icon_data[index]" >
                                    </b-col>
                                    <b-col class="result_cell" v-for="(row, key) in col" :key="row">
                                        <span v-if=" index != key ">
                                            <img class="result_icon" v-if="row == 'red_1'" src="@/assets/img/game_icon/red_icon_+.png">
                                            <img class="result_icon" v-else-if="row == 'red_0'" src="@/assets/img/game_icon/red_icon_-.png">
                                            <img class="result_icon" v-else-if="row == 'blue_1'" src="@/assets/img/game_icon/blue_icon_+.png">
                                            <img class="result_icon" v-else-if="row == 'blue_0'" src="@/assets/img/game_icon/blue_icon_-.png">
                                            <img class="result_icon" v-else-if="row == 'green_1'" src="@/assets/img/game_icon/green_icon_+.png">
                                            <img class="result_icon" v-else-if="row == 'green_0'" src="@/assets/img/game_icon/green_icon_-.png">
                                            <img class="result_icon" v-else-if="row == 'blank'" src="@/assets/img/game_icon/blank_icon.png">
                                        </span>
                                        <span v-else-if=" index == key ">
                                            
                                        </span>
                                    </b-col>
                                </b-row>
                            </b-col>

                            <b-col>
                                <b-row>
                                    <b-col>
                                    </b-col>
                                    <b-col class="border-set" v-for="n in 8" :key="n">
                                        <img class="ingredient_icon" :src="this.icon_data[n-1]">
                                    </b-col>
                                </b-row>
                                <b-row v-for="(col, index) in data.ingredient_reasoning" :key="col">
                                    <b-col class="border-set">
                                        <img class="result_element" :src="reasoning_data[index]" >
                                    </b-col>
                                    <b-col class="result_cell btn_cell_opt" 
                                        v-on:click="open_reasoning_table_modal(index, key)"
                                        v-for="(row, key) in col" :key="row">
                                        <span v-if=" row == 0 ">
                                        </span>
                                        <span v-else-if=" row == 1 "  class="marking_letter">
                                            X
                                        </span>
                                        <span v-else-if=" row == 2 " class="marking_letter">
                                            O
                                        </span>
                                    </b-col>
                                </b-row>
                            </b-col>
                        </b-row>
                    </b-container>
                </div>
            </div>
        </b-modal>

        <!-- 추리 테이블 칸 변경 모달 -->
        <b-modal id="reasoning_table_modal" 
            centered 
            ref="reasoning_table_modal"
            v-model="reasoning_table_onoff"
            header-text-variant="light"
            header-bg-variant="danger"
            hide-footer
            hide-header-close
            title="골라보자"
            >
            <div id="reasoning_table_modal_intitle">
                <p>
                    현재 선택된 칸
                    <span ref="index_val"></span>
                    ,
                    <span ref="key_val"></span>
                </p>
                <p>어떤 것을 고르시겠습니까?</p>
                <div id="select_value_area">
                    <input type="radio" class="select_radio" id="blank" value="0" v-model="picked" />
                    <label for="blank">빈 칸</label>
                    <input type="radio" class="select_radio" id="x_val" value="1" v-model="picked" />
                    <label for="x_val">X</label>
                    <input type="radio" class="select_radio" id="o_val" value="2" v-model="picked" />
                    <label for="o_val">O</label>
                </div>
                <br>
            </div>
            <div id="decide_reasoning_select_btn_area">
                <b-button variant="outline-success" block class="reasoning_table_button" 
                    @click="click_reasoning_ele(this.$refs.index_val.innerText, this.$refs.key_val.innerText)"
                    >
                    결정
                </b-button>
                <b-button variant="outline-danger" v-on:click="reset_val()" class="reasoning_table_button" data-bs-dismiss="modal">취소</b-button>
            </div> 
        </b-modal>

        <!-- 좌측 offcanvas  -->
        <Personal_game_data 
            v-bind:user_data="this.user_data"
            v-bind:my_key="this.my_key"
            v-on:favor_card_modal_on="favor_card_modal_on"
        >
        </Personal_game_data>

        <!-- 모달 컴포넌트 -->
        <Modal_component
            @click_artifact_ok="buy_artifact_confirm"
        >
        </Modal_component>

    </div>
</keep-alive>
</template>

<script src="@/assets/script/game_board.js">
</script>


<style scoped src="@/assets/css/game_board.css"></style>