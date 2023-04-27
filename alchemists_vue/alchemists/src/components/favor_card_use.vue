<template>
    <div>
        <b-modal
            id="favor_card_use_check_modal"
            ref="favor_card_use_check_modal"
            v-model="favor_card_use_check_modal"
            centered
            lazy
            size="lg"
            header-text-variant="light"
            header-bg-variant="success"
            hide-footer
            hide-header-close
            hide-backdrop
            no-close-on-backdrop
            title="호의 카드"
        >
        
        <b-container v-if="this.card_kind == 'assistent'">
            <!-- 조수 -->
            <b-row>
                <b-col>
                    <img class="favor_card_img_class" src="@/assets/img/favor_card/assistent.png" />
                </b-col>
                <b-col>
                    <b-row>
                        <b-col>
                            <p class="explain_title">
                                상세설명
                            </p>
                        </b-col>
                    </b-row>
                    <b-row>
                        <b-col>
                            <p class="explain_p"> 행동을 결정할 때 사용 :: 큐브를 놓을 때 사용 </p>
                            <p class="explain_p"> 이번 라운드에 <br> 행동 큐브 1개를 <br> 추가로 사용합니다.</p>
                        </b-col>
                    </b-row>
                </b-col>
            </b-row>
            <b-row>
                <b-col>
                    <b-button variant="outline-success" v-if="this.can_use == true" class="favor_card_btn" v-on:click="use_favor_card('assistent')">
                        사용하기
                    </b-button>
                    <b-button variant="outline-danger"  block class="favor_card_btn" v-on:click="favor_modal_close">
                        닫기
                    </b-button>
                </b-col>
            </b-row>
        </b-container>

        <b-container v-if="this.card_kind == 'herbalist'">
            <!-- 약초학자 -->
            <b-row>
                <b-col>
                    <img class="favor_card_img_class" src="@/assets/img/favor_card/herbalist.png" />
                </b-col>
                <b-col>
                    <b-row>
                        <b-col>
                            <p class="explain_title">
                                상세설명
                            </p>
                        </b-col>
                    </b-row>
                    <b-row>
                        <b-col>
                            <p class="explain_p"> 즉시 사용 </p>
                            <p class="explain_p"> 재료 카드 3장을 얻고 2장을 버립니다. </p>
                        </b-col>
                    </b-row>
                    <b-row v-if="this.can_use == true">
                        <b-col>
                            버릴 2장을 골라주세요!
                        </b-col>
                    </b-row>
                    <b-row v-if="this.can_use == true">
                        <b-col>
                            <div class="dump_ingre_div" v-for="(card, name) in this.my_data" :key="name" >
                                <img class="dump_ingre_icon" ref="ingre_1" v-if="card > 0 && name == 'card_1' "      src="@/assets/img/material_card/card_1.png" v-on:click="click_ingre_for_sell(name)" />
                                <img class="dump_ingre_icon" ref="ingre_2" v-else-if="card > 0 && name == 'card_2' " src="@/assets/img/material_card/card_2.png" v-on:click="click_ingre_for_sell(name)" />
                                <img class="dump_ingre_icon" ref="ingre_3" v-else-if="card > 0 && name == 'card_3' " src="@/assets/img/material_card/card_3.png" v-on:click="click_ingre_for_sell(name)" />
                                <img class="dump_ingre_icon" ref="ingre_4" v-else-if="card > 0 && name == 'card_4' " src="@/assets/img/material_card/card_4.png" v-on:click="click_ingre_for_sell(name)" /> 
                                <img class="dump_ingre_icon" ref="ingre_5" v-else-if="card > 0 && name == 'card_5' " src="@/assets/img/material_card/card_5.png" v-on:click="click_ingre_for_sell(name)" /> 
                                <img class="dump_ingre_icon" ref="ingre_6" v-else-if="card > 0 && name == 'card_6' " src="@/assets/img/material_card/card_6.png" v-on:click="click_ingre_for_sell(name)" /> 
                                <img class="dump_ingre_icon" ref="ingre_7" v-else-if="card > 0 && name == 'card_7' " src="@/assets/img/material_card/card_7.png" v-on:click="click_ingre_for_sell(name)" /> 
                                <img class="dump_ingre_icon" ref="ingre_8" v-else-if="card > 0 && name == 'card_8' " src="@/assets/img/material_card/card_8.png" v-on:click="click_ingre_for_sell(name)" />
                                <p class="dump_ingre_num"> {{ card }}장</p>
                            </div>
                        </b-col>
                    </b-row>
                </b-col>
            </b-row>
            <b-row>
                <b-col>
                    <b-button variant="outline-success" v-if="this.can_use == true" class="favor_card_btn" v-on:click="use_favor_card('herbalist')">
                        사용하기
                    </b-button>
                    <b-button variant="outline-danger"  block class="favor_card_btn" v-on:click="favor_modal_close">
                        닫기
                    </b-button>
                </b-col>
            </b-row>
        </b-container>

        <b-container v-if="this.card_kind == 'bar_owner'">
            <!-- 술집점원 -->
            <b-row>
                <b-col>
                    <img class="favor_card_img_class" src="@/assets/img/favor_card/bar_owner.png" />
                </b-col>
                <b-col>
                    <b-row>
                        <b-col>
                            <p class="explain_title">
                                상세설명
                            </p>
                        </b-col>
                    </b-row>
                    <b-row>
                        <b-col>
                            <p class="explain_p"> 모험가에게 물약을 팔 차례에 사용 </p>
                            <p class="explain_p"> 정확히 일치하는 물약을 만들면 <br> <strong>명성 1점</strong>을 획득합니다.</p>
                            <p class="explain_p"> 일치하지 않으면 그 물약은 원래보다 1단계 좋은 물약으로 치게 됩니다. </p>
                        </b-col>
                    </b-row>
                </b-col>
            </b-row>
            <b-row>
                <b-col>
                    <b-button variant="outline-success" v-if="this.can_use == true" class="favor_card_btn" v-on:click="use_favor_card('bar_owner')">
                        사용하기
                    </b-button>
                    <b-button variant="outline-danger"  block class="favor_card_btn" v-on:click="favor_modal_close">
                        닫기
                    </b-button>
                </b-col>
            </b-row>
        </b-container>

        <b-container v-if="this.card_kind == 'wise_man'">
            <!-- 현자 -->
            <b-row>
                <b-col>
                    <img class="favor_card_img_class" src="@/assets/img/favor_card/wise_man.png" />
                </b-col>
                <b-col>
                    <b-row>
                        <b-col>
                            <p class="explain_title">
                                상세설명
                            </p>
                        </b-col>
                    </b-row>
                    <b-row>
                        <b-col>
                            <p class="explain_p"> 재료를 변환 할 때 사용 :: 재료를 판매할 때 사용 </p>
                            <p class="explain_p"> 재료를 변환하면서 금화 1개를 추가로 받습니다. </p>
                        </b-col>
                    </b-row>
                </b-col>
            </b-row>
            <b-row>
                <b-col>
                    <b-button variant="outline-success" v-if="this.can_use == true" class="favor_card_btn" v-on:click="use_favor_card('wise_man')">
                        사용하기
                    </b-button>
                    <b-button variant="outline-danger"  block class="favor_card_btn" v-on:click="favor_modal_close">
                        닫기
                    </b-button>
                </b-col>
            </b-row>
        </b-container>

        <b-container v-if="this.card_kind == 'merchant'">
            <!-- 상인 -->
            <b-row>
                <b-col>
                    <img class="favor_card_img_class" src="@/assets/img/favor_card/merchant.png" />
                </b-col>
                <b-col>
                    <b-row>
                        <b-col>
                            <p class="explain_title">
                                상세설명
                            </p>
                        </b-col>
                    </b-row>
                    <b-row>
                        <b-col>
                            <p class="explain_p"> 자신의 물약을 팔 차례에 사용 </p>
                            <p class="explain_p"> 이번 라운드에 자신이 처음으로 물약을 팔면 금화 1개를 받습니다.</p>
                            <p class="explain_p"> 첫 판매자가 아니라면 첫 판매자처럼 3가지 물약 중 원하는 물약을 골라 팔 수 있습니다. </p>
                        </b-col>
                    </b-row>
                </b-col>
            </b-row>
            <b-row>
                <b-col>
                    <b-button variant="outline-success" v-if="this.can_use == true" class="favor_card_btn" v-on:click="use_favor_card('merchant')">
                        사용하기
                    </b-button>
                    <b-button variant="outline-danger"  block class="favor_card_btn" v-on:click="favor_modal_close">
                        닫기
                    </b-button>
                </b-col>
            </b-row>
        </b-container>

        <b-container v-if="this.card_kind == 'shopkeeper'">
            <!-- 가게주인 -->
            <b-row>
                <b-col>
                    <img class="favor_card_img_class" src="@/assets/img/favor_card/shopkeeper.png" />
                </b-col>
                <b-col>
                    <b-row>
                        <b-col>
                            <p class="explain_title">
                                상세설명
                            </p>
                        </b-col>
                    </b-row>
                    <b-row>
                        <b-col>
                            <p class="explain_p"> 아이템을 살 때 사용 </p>
                            <p class="explain_p"> 아이템을 구매할 때 금화 한 개를 적게 냅니다. </p>
                            <p class="explain_p"> :: 금화 1개 할인 카드 </p>
                        </b-col>
                    </b-row>
                </b-col>
            </b-row>
            <b-row>
                <b-col>
                    <b-button variant="outline-success" v-if="this.can_use == true" class="favor_card_btn" v-on:click="use_favor_card('shopkeeper')">
                        사용하기
                    </b-button>
                    <b-button variant="outline-danger"  block class="favor_card_btn" v-on:click="favor_modal_close">
                        닫기
                    </b-button>
                </b-col>
            </b-row>
        </b-container>

        <b-container v-if="this.card_kind == 'caretaker'">
            <!-- 관리인 -->
            <b-row>
                <b-col>
                    <img class="favor_card_img_class" src="@/assets/img/favor_card/caretaker.png" />
                </b-col>
                <b-col>
                    <b-row>
                        <b-col>
                            <p class="explain_title">
                                상세설명
                            </p>
                        </b-col>
                    </b-row>
                    <b-row>
                        <b-col>
                            <p class="explain_p"> 행동을 결정할 때 사용 :: 큐브를 놓을 때 사용 </p>
                            <p class="explain_p"> 이 카드를 통해 큐브 하나를 킵합니다.</p>
                            <p class="explain_p"> 이번 라운드에 물약 팔기 행동을 하기 전에 킵된 큐브를 사용해 물약 마시기 행동을 할 수 있습니다.</p>
                            <p class="explain_p"> 라운드가 끝나면 카드는 무효화되고 킵한 큐브는 주인에게 돌아가게 됩니다. </p>
                        </b-col>
                    </b-row>
                </b-col>
            </b-row>
            <b-row>
                <b-col>
                    <b-button variant="outline-success" v-if="this.can_use == true" class="favor_card_btn" v-on:click="use_favor_card('caretaker')">
                        사용하기
                    </b-button>
                    <b-button variant="outline-danger"  block class="favor_card_btn" v-on:click="favor_modal_close">
                        닫기
                    </b-button>
                </b-col>
            </b-row>
        </b-container>

        <b-container v-if="this.card_kind == 'big_man'">
            <!-- 힘센친구 -->
            <b-row>
                <b-col>
                    <img class="favor_card_img_class" src="@/assets/img/favor_card/big_man.png" />
                </b-col>
                <b-col>
                    <b-row>
                        <b-col>
                            <p class="explain_title">
                                상세설명
                            </p>
                        </b-col>
                    </b-row>
                    <b-row>
                        <b-col>
                            <p class="explain_p"> 행동을 결정할 때 사용 :: 큐브를 놓을 때 사용 </p>
                            <p class="explain_p"> 행동 칸 하나를 선택해서, 그곳에 있는 자신의 큐브를 가장 윗줄로 올립니다.</p>
                        </b-col>
                    </b-row>
                </b-col>
            </b-row>
            <b-row v-if="this.can_use == true">
                <b-col>
                    <div>
                        <img class="favor_board_icon" ref="board_1" src="@/assets/img/game_icon/select_items_icon.png" :click="click_select_board(1)" />
                    </div>
                    <div>
                        <span class="favor_board_ment">재료 선택</span>
                    </div>
                </b-col>
                <b-col>
                    <div>
                        <img class="favor_board_icon" ref="board_2" src="@/assets/img/game_icon/sell_item_icon.png"   v-on:click="click_select_board(2)" />
                    </div>
                    <div>
                        <span class="favor_board_ment">재료 판매</span>
                    </div>
                </b-col>
                <b-col>
                    <div>
                        <img class="favor_board_icon" ref="board_3" src="@/assets/img/adventurer/adventurer_back.png" v-on:click="click_select_board(3)" />
                    </div>
                    <div>
                        <span class="favor_board_ment">용병에게 <br>물약 판매</span>
                    </div>
                </b-col>
                <b-col>
                    <div>
                        <img class="favor_board_icon" ref="board_4" src="@/assets/img/game_icon/buy_item_icon.png" v-on:click="click_select_board(4)" />
                    </div>
                    <div>
                        <span class="favor_board_ment">아이템 구입</span>
                    </div>
                </b-col>
                <b-col>
                    <div>
                        <img class="favor_board_icon" ref="board_5" src="@/assets/img/game_icon/refuting_a_theory_icon.png" v-on:click="click_select_board(5)" />
                    </div>
                    <div>
                        <span class="favor_board_ment">학설 반박</span>
                    </div>
                </b-col>
                <b-col>
                    <div>
                        <img class="favor_board_icon" ref="board_6" src="@/assets/img/game_icon/presentation_of_theories_icon.png" v-on:click="click_select_board(6)" />
                    </div>
                    <div>
                        <span class="favor_board_ment">논문 발표</span>
                    </div>
                </b-col>
                <b-col>
                    <div>
                        <img class="favor_board_icon" ref="board_7" src="@/assets/img/game_icon/test_student_icon.png" v-on:click="click_select_board(7)" />
                    </div>
                    <div>
                        <span class="favor_board_ment">학생에게<br>실험하기</span>
                    </div>
                </b-col>
                <b-col>
                    <div>
                        <img class="favor_board_icon" ref="board_8" src="@/assets/img/game_icon/test_i_icon.png" v-on:click="click_select_board(8)" />
                    </div>
                    <div>
                        <span class="favor_board_ment">스스로<br>시험하기</span>
                    </div>
                </b-col>
            </b-row>
            <b-row>
                <b-col>
                    <b-button variant="outline-success" v-if="this.can_use == true" class="favor_card_btn" v-on:click="use_favor_card('big_man')">
                        사용하기
                    </b-button>
                    <b-button variant="outline-danger"  block class="favor_card_btn" v-on:click="favor_modal_close">
                        닫기
                    </b-button>
                </b-col>
            </b-row>
        </b-container>

        </b-modal>
    </div>
</template>

<style scoped src="@/assets/css/favor_card_use.css">
</style>


<script src="@/assets/script/favor_card_use.js">
</script>
