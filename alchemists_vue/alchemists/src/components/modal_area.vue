<template>
    <div class="modal_area">
        
        <b-modal class="artifacts_card_modal" id="bronze_cup" centered ref="bronze_cup" title="청동잔" ok-title="구매" @ok="buy_artifact('rank_3',0)">
            <b-container>
                <b-row>
                    <b-col cols="4">
                        <img class="origin_img" src="@/assets/img/artifacts/rank_3/bronze_cup.png">
                    </b-col>
                    <b-col>
                        <b-row>
                            <b-col>
                                <p> <img class="token_icon" src="@/assets/img/game_icon/gold_icon.png"> 4 </p>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col>
                                <p> <img class="token_icon" src="@/assets/img/game_icon/fame_icon.png"> 4 </p>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col>
                                <p> 이 아이템은 특별한 효과는 없지만 승점을 제공합니다. </p>
                            </b-col>
                        </b-row>
                    </b-col>
                </b-row>
            </b-container>
        </b-modal>
        <b-modal class="artifacts_card_modal" id="chest_of_witch" centered ref="chest_of_witch" title="마녀의 궤짝" ok-title="구매" @ok="buy_artifact('rank_2',0)">
            <b-container>
                <b-row>
                    <b-col cols="4">
                        <img class="origin_img" src="@/assets/img/artifacts/rank_2/chest_of_witch.png">
                    </b-col>
                    <b-col>
                        <b-row>
                            <b-col>
                                <p> <img class="token_icon" src="@/assets/img/game_icon/gold_icon.png"> 3 </p>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col>
                                <p> <img class="token_icon" src="@/assets/img/game_icon/fame_icon.png"> 2 </p>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col>
                                <p> <strong>즉시 효과를 적용</strong> </p>
                                <p> 재료카드 7장을 뽑습니다. </p>
                                <p> 대신, 이후부터 플레이 순서를 정할 때 재료카드를 받을 수 없습니다. </p>
                            </b-col>
                        </b-row>
                    </b-col>
                </b-row>
            </b-container>
        </b-modal>
        <b-modal class="artifacts_card_modal" id="discount_card" centered ref="discount_card" title="할인 카드" ok-title="구매" @ok="buy_artifact('rank_1',0)">
            <b-container>
                <b-row>
                    <b-col cols="4">
                        <img class="origin_img" src="@/assets/img/artifacts/rank_1/discount_card.png">
                    </b-col>
                    <b-col>
                        <b-row>
                            <b-col>
                                <p> <img class="token_icon" src="@/assets/img/game_icon/gold_icon.png"> 3 </p>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col>
                                <p> <img class="token_icon" src="@/assets/img/game_icon/fame_icon.png"> 1 </p>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col>
                                <p> 다음에 구매하는 유물은 금화 2개를 적게 냅니다. </p>
                                <p> 그 다음 아이템들은 금화를 1개 적게 냅니다. </p>
                            </b-col>
                        </b-row>
                    </b-col>
                </b-row>
            </b-container>
        </b-modal>
        <b-modal class="artifacts_card_modal" id="eloquent_necklace" centered ref="eloquent_necklace" title="달변가의 목걸이" ok-title="구매" @ok="buy_artifact('rank_2',1)">
            <b-container>
                <b-row>
                    <b-col cols="4">
                        <img class="origin_img" src="@/assets/img/artifacts/rank_2/eloquent_necklace.png">
                    </b-col>
                    <b-col>
                        <b-row>
                            <b-col>
                                <p> <img class="token_icon" src="@/assets/img/game_icon/gold_icon.png"> 4 </p>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col>
                                <p> <img class="token_icon" src="@/assets/img/game_icon/fame_icon.png"> 0 </p>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col>
                                <p> <strong>즉시 효과를 적용</strong> </p>
                                <p> 명성 점수 5점을 얻습니다. </p>
                            </b-col>
                        </b-row>
                    </b-col>
                </b-row>
            </b-container>
        </b-modal>
        <b-modal class="artifacts_card_modal" id="feather_hat" centered ref="feather_hat" title="깃털 모자" ok-title="구매" @ok="buy_artifact('rank_3',1)">
            <b-container>
                <b-row>
                    <b-col cols="4">
                        <img class="origin_img" src="@/assets/img/artifacts/rank_3/feather_hat.png">
                    </b-col>
                    <b-col>
                        <b-row>
                            <b-col>
                                <p> <img class="token_icon" src="@/assets/img/game_icon/gold_icon.png"> 3 </p>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col>
                                <p> <img class="token_icon" src="@/assets/img/game_icon/fame_icon.png"> * </p>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col>
                                <p> <strong> 전시회 동안 발동 </strong> </p>
                                <p> 성공적으로 입증한 물약의 재료들을 따로 저장합니다. 아이템 점수를 계산 할 때 따로 둔 재료의 종류마다 승점 1점을 획득합니다.</p>
                            </b-col>
                        </b-row>
                    </b-col>
                </b-row>
            </b-container>
        </b-modal>
        <b-modal class="artifacts_card_modal" id="glass_cabinet" centered ref="glass_cabinet" title="유리 장식장" ok-title="구매" @ok="buy_artifact('rank_3',2)">
            <b-container>
                <b-row>
                    <b-col cols="4">
                        <img class="origin_img" src="@/assets/img/artifacts/rank_3/glass_cabinet.png">
                    </b-col>
                    <b-col>
                        <b-row>
                            <b-col>
                                <p> <img class="token_icon" src="@/assets/img/game_icon/gold_icon.png"> 5 </p>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col>
                                <p> <img class="token_icon" src="@/assets/img/game_icon/fame_icon.png"> * </p>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col>
                                <p> 아이템 점수를 계산할 때, 이 카드를 포함하여 </p>
                                <p> 자신이 가진 아이템마다 승점 2점을 얻습니다. </p>
                            </b-col>
                        </b-row>
                    </b-col>
                </b-row>
            </b-container>
        </b-modal>
        <b-modal class="artifacts_card_modal" id="golden_alter" centered ref="golden_alter" title="황금 제단" ok-title="구매" @ok="buy_artifact('rank_3',3)">
            <b-container>
                <b-row>
                    <b-col cols="4">
                        <img class="origin_img" src="@/assets/img/artifacts/rank_3/golden_alter.png">
                    </b-col>
                    <b-col>
                        <b-row>
                            <b-col>
                                <p> <img class="token_icon" src="@/assets/img/game_icon/gold_icon.png"> 1 </p>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col>
                                <p> <img class="token_icon" src="@/assets/img/game_icon/fame_icon.png"> 0 </p>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col>
                                <p> <strong>즉시 효과를 적용</strong> </p>
                                <p> 금화를 1개부터 8개까지 쓸 수 있습니다. </p>
                                <p> 사용한 금화만큼 명성을 획득합니다. </p>
                            </b-col>
                        </b-row>
                    </b-col>
                </b-row>
            </b-container>
        </b-modal>
        <b-modal class="artifacts_card_modal" id="haste_boots" centered ref="haste_boots" title="속도 향상 장화" ok-title="구매" @ok="buy_artifact('rank_1',1)">
            <b-container>
                <b-row>
                    <b-col cols="4">
                        <img class="origin_img" src="@/assets/img/artifacts/rank_1/haste_boots.png">
                    </b-col>
                    <b-col>
                        <b-row>
                            <b-col>
                                <p> <img class="token_icon" src="@/assets/img/game_icon/gold_icon.png"> 4 </p>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col>
                                <p> <img class="token_icon" src="@/assets/img/game_icon/fame_icon.png"> 2 </p>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col>
                                <p> 자신의 큐브가 하나라도 있는 행동 칸 한 곳에서 모두가 행동을 마친뒤 </p>
                                <p> 한 번 더 그 행동을 할 수 있습니다. </p>
                                <p> 라운드 당 한 번만 사용 가능합니다. </p>
                                <p> 물약을 팔 때는 쓸 수 없습니다. </p>
                            </b-col>
                        </b-row>
                    </b-col>
                </b-row>
            </b-container>
        </b-modal>
        <b-modal class="artifacts_card_modal" id="hypnotic_necklace" centered ref="hypnotic_necklace" title="최면술의 목걸이" ok-title="구매" @ok="buy_artifact('rank_2',2)">
            <b-container>
                <b-row>
                    <b-col cols="4">
                        <img class="origin_img" src="@/assets/img/artifacts/rank_2/hypnotic_necklace.png">
                    </b-col>
                    <b-col>
                        <b-row>
                            <b-col>
                                <p> <img class="token_icon" src="@/assets/img/game_icon/gold_icon.png"> 3 </p>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col>
                                <p> <img class="token_icon" src="@/assets/img/game_icon/fame_icon.png"> 1 </p>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col>
                                <p> <strong>즉시 효과를 적용</strong> </p>
                                <p> 호의카드 4장을 받습니다. </p>
                            </b-col>
                        </b-row>
                    </b-col>
                </b-row>
            </b-container>
        </b-modal>
        <b-modal class="artifacts_card_modal" id="magic_mirror" centered ref="magic_mirror" title="마법 거울" ok-title="구매" @ok="buy_artifact('rank_3',4)">
            <b-container>
                <b-row>
                    <b-col cols="4">
                        <img class="origin_img" src="@/assets/img/artifacts/rank_3/magic_mirror.png">
                    </b-col>
                    <b-col>
                        <b-row>
                            <b-col>
                                <p> <img class="token_icon" src="@/assets/img/game_icon/gold_icon.png"> 4 </p>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col>
                                <p> <img class="token_icon" src="@/assets/img/game_icon/fame_icon.png"> * </p>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col>
                                <p> 아이템 점수를 계산 할 때, </p>
                                <p> 라운드가 끝날 당시 보유했던 명성 5점마다 승점 1점을 획득합니다. </p>
                            </b-col>
                        </b-row>
                    </b-col>
                </b-row>
            </b-container>
        </b-modal>
        <b-modal class="artifacts_card_modal" id="magic_mortar" centered ref="magic_mortar" title="마법의 절구" ok-title="구매" @ok="buy_artifact('rank_1',2)">
            <b-container>
                <b-row>
                    <b-col cols="4">
                        <img class="origin_img" src="@/assets/img/artifacts/rank_1/magic_mortar.png">
                    </b-col>
                    <b-col>
                        <b-row>
                            <b-col>
                                <p> <img class="token_icon" src="@/assets/img/game_icon/gold_icon.png"> 3 </p>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col>
                                <p> <img class="token_icon" src="@/assets/img/game_icon/fame_icon.png"> 1 </p>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col>
                                <p> 물약을 만들 때, </p>
                                <p> 재료 중 한 개만 버립니다. </p>
                                <p> 버릴 재료는 다른 사람이 무작위로 고릅니다. </p>
                            </b-col>
                        </b-row>
                    </b-col>
                </b-row>
            </b-container>
        </b-modal>
        <b-modal class="artifacts_card_modal" id="night_vision" centered ref="night_vision" title="잠망경" ok-title="구매" @ok="buy_artifact('rank_1',3)">
            <b-container>
                <b-row>
                    <b-col cols="4">
                        <img class="origin_img" src="@/assets/img/artifacts/rank_1/night_vision.png">
                    </b-col>
                    <b-col>
                        <b-row>
                            <b-col>
                                <p> <img class="token_icon" src="@/assets/img/game_icon/gold_icon.png"> 3 </p>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col>
                                <p> <img class="token_icon" src="@/assets/img/game_icon/fame_icon.png"> 1 </p>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col>
                                <p> 다른 사람이 물약을 팔거나, </p>
                                <p> 실험한 그 즉시 재료 중 하나를 볼 수 있습니다. </p>
                                <p> 볼 재료는 무작위로 고릅니다. </p>
                                <p> <strong>라운드 당 한 번만 사용합니다.</strong> </p>
                            </b-col>
                        </b-row>
                    </b-col>
                </b-row>
            </b-container>
        </b-modal>
        <b-modal class="artifacts_card_modal" id="printing_machine" centered ref="printing_machine" title="인쇄기" ok-title="구매" @ok="buy_artifact('rank_1',4)">
            <b-container>
                <b-row>
                    <b-col cols="4">
                        <img class="origin_img" src="@/assets/img/artifacts/rank_1/printing_machine.png">
                    </b-col>
                    <b-col>
                        <b-row>
                            <b-col>
                                <p> <img class="token_icon" src="@/assets/img/game_icon/gold_icon.png"> 4 </p>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col>
                                <p> <img class="token_icon" src="@/assets/img/game_icon/fame_icon.png"> 2 </p>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col>
                                <p> 학설을 발표하거나, </p>
                                <p> 지지할 때 </p>
                                <p> 금화 한 개를 지불하지 않습니다. </p>
                            </b-col>
                        </b-row>
                    </b-col>
                </b-row>
            </b-container>
        </b-modal>
        <b-modal class="artifacts_card_modal" id="robe_of_respect" centered ref="robe_of_respect" title="존경의 외투" ok-title="구매" @ok="buy_artifact('rank_1',5)">
            <b-container>
                <b-row>
                    <b-col cols="4">
                        <img class="origin_img" src="@/assets/img/artifacts/rank_1/robe_of_respect.png">
                    </b-col>
                    <b-col>
                        <b-row>
                            <b-col>
                                <p> <img class="token_icon" src="@/assets/img/game_icon/gold_icon.png"> 4 </p>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col>
                                <p> <img class="token_icon" src="@/assets/img/game_icon/fame_icon.png"> 0 </p>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col>
                                <p> 명성 점수를 얻을 때 마다, </p>
                                <p> 1점을 더 얻습니다. </p>
                                <p> 마지막 라운드는 적용하지 않습니다. </p>
                            </b-col>
                        </b-row>
                    </b-col>
                </b-row>
            </b-container>
        </b-modal>
        <b-modal class="artifacts_card_modal" id="seal_of_authority" centered ref="seal_of_authority" title="권위의 인장" ok-title="구매" @ok="buy_artifact('rank_2',3)">
            <b-container>
                <b-row>
                    <b-col cols="4">
                        <img class="origin_img" src="@/assets/img/artifacts/rank_2/seal_of_authority.png">
                    </b-col>
                    <b-col>
                        <b-row>
                            <b-col>
                                <p> <img class="token_icon" src="@/assets/img/game_icon/gold_icon.png"> 4 </p>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col>
                                <p> <img class="token_icon" src="@/assets/img/game_icon/fame_icon.png"> 0 </p>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col>
                                <p> 학설을 발효하거나, </p>
                                <p> 지지할 때 마다 </p>
                                <p> 명성 2점을 추가로 얻습니다. </p>
                            </b-col>
                        </b-row>
                    </b-col>
                </b-row>
            </b-container>
        </b-modal>
        <b-modal class="artifacts_card_modal" id="silver_glass" centered ref="silver_glass" title="은 잔" ok-title="구매" @ok="buy_artifact('rank_2',4)">
            <b-container>
                <b-row>
                    <b-col cols="4">
                        <img class="origin_img" src="@/assets/img/artifacts/rank_2/silver_glass.png">
                    </b-col>
                    <b-col>
                        <b-row>
                            <b-col>
                                <p> <img class="token_icon" src="@/assets/img/game_icon/gold_icon.png"> 4 </p>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col>
                                <p> <img class="token_icon" src="@/assets/img/game_icon/fame_icon.png"> 6 </p>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col>
                                <p> 이 아이템은 특별한 효과는 없지만 승점을 제공합니다. </p>
                            </b-col>
                        </b-row>
                    </b-col>
                </b-row>
            </b-container>
        </b-modal>
        <b-modal class="artifacts_card_modal" id="statue_of_wisdom" centered ref="statue_of_wisdom" title="지혜의 조각상" ok-title="구매" @ok="buy_artifact('rank_3',5)">
            <b-container>
                <b-row>
                    <b-col cols="4">
                        <img class="origin_img" src="@/assets/img/artifacts/rank_3/statue_of_wisdom.png">
                    </b-col>
                    <b-col>
                        <b-row>
                            <b-col>
                                <p> <img class="token_icon" src="@/assets/img/game_icon/gold_icon.png"> 4 </p>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col>
                                <p> <img class="token_icon" src="@/assets/img/game_icon/fame_icon.png"> * </p>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col>
                                <p> 게임이 끝날 때, </p>
                                <p> 옳은 학설에 놓인 자신의 인장마다 승점 1점을 얻습니다. </p>
                            </b-col>
                        </b-row>
                    </b-col>
                </b-row>
            </b-container>
        </b-modal>
        <b-modal class="artifacts_card_modal" id="thinking_hat" centered ref="thinking_hat" title="생각하는 모자" ok-title="구매" @ok="buy_artifact('rank_2',5)">
            <b-container>
                <b-row>
                    <b-col cols="4">
                        <img class="origin_img" src="@/assets/img/artifacts/rank_2/thinking_hat.png">
                    </b-col>
                    <b-col>
                        <b-row>
                            <b-col>
                                <p> <img class="token_icon" src="@/assets/img/game_icon/gold_icon.png"> 4 </p>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col>
                                <p> <img class="token_icon" src="@/assets/img/game_icon/fame_icon.png"> 1 </p>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col>
                                <p> <strong>즉시 효과를 적용</strong> </p>
                                <p> 자신이 가진 재료카드를 최대 2쌍 실헙합니다. </p>
                                <p> 하나의 재료 카드를 2쌍에 동시에 사용할 수 없습니다. </p>
                                <p> 재료 카드를 버리지 않습니다. </p>
                            </b-col>
                        </b-row>
                    </b-col>
                </b-row>
            </b-container>
        </b-modal>

        <b-modal class="cube_rule_modal" id="fame_of_prove" centered ref="fame_of_prove" title="최고의 연금술사 상">
            <div>
                학설 보드에 인장을 가장 많이 놓은 플레이어는 명성 점수 1점을 얻습니다. <br>
                학설이 하나도 발표되지 않았으면 아무도 이 보상을 받지 못합니다. <br>
                가장 많이 놓은 사람이 여럿인 경우, 해당 플레이어들은 함께 명성 점수 1점을 받습니다. <br>
                이때, 직접 발표한 인장과 지지한 인장의 차이는 없습니다. <br>
                마찬가지로, 인장이 공개되지 않기 때문에 별표의 유무도 관계없습니다. 
            </div>
        </b-modal>
        <b-modal class="cube_rule_modal" id="none_use_cube" centered ref="none_use_cube" title="쓰지 않은 큐브">
            <div>
                라운드가 끝났을 때, 이 칸에 있는 자신의 큐브 한 쌍당 호의 카드 1장을 받습니다. <br>
                쌍을 이루지 못한 큐브로는 아무것도 받지 못합니다
            </div>
        </b-modal>
        <b-modal class="cube_rule_modal" id="hospital" centered ref="hospital" title="병원">
            <div>
                쓰지 않은 큐브 칸이 비워지면, 즉시 병원에 있는 큐브를 전부 쓰지 않은 큐브 칸으로 옮깁니다. <br>
                그 큐브들은 다음 라운드가 끝날 때, 쓰지 않은 큐브로 간주합니다.
            </div>
        </b-modal>
    </div>
</template>

<script>
export default {
    name: 'modal_area',
    methods : {
        buy_artifact : function(rank, number) {
            let data = {
                rank : rank,
                num : number
            }
            this.$emit('click_artifact_ok' , data);
        }
    },
}

</script>

<style>
div.favor_card_modal {
    font-family : '양진체';
}

img.token_icon {
    max-width: 40px;
}
img.origin_img {
    max-width: 120px;
}

</style>