using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DumbAI : MonoBehaviour
{
    private Game g;
    private Player p;
    public int epsilon;
    public int turnCounter;
    private List<StateObj> memory;

    // Start is called before the first frame update
    public void initAI(Player p, Game g)
    {
        memory = new List<StateObj>();
        this.g = g;
        this.p = p;

        //epsilon is set to give randomness to actions
        epsilon = 12;
    }

    public void takeTurn()
    {
        if(p.getHand().Count == 0)
        {
            p.drawCard();
        }
        else if(epsilon > 0)
        {
            playRandomCard();
        }
        else
        {
            Card c = findMatch();
            if (c != null)
            {
                Card topCard = g.getDiscard().getTopCard();
                string outcome = GameActions.npcCheckCard(c, p, g.getCurrentTurn(), g.getTurnDirection(), g.getDiscard(), g.displayText);
                int reward = setReward(p, outcome);
                addToMemory(c, topCard, "", reward);
            }
            else
                playRandomCard();
        }
    }

    private int setReward(Player p, string outcome)
    {
        int reward = 0;
        if (outcome == "Correct play")
        {
            reward += 10;
        }
        else if (outcome != "Press draw card")
        {
            reward -= 10;
        }

        return reward;
    }

    public void addToMemory(Card cardPlayed, Card topCard, string postAction, int reward)
    {
        if (!hasState(cardPlayed, topCard, postAction, reward))
        {
            memory.Add(new StateObj(cardPlayed, topCard, postAction, reward));
        }
    }

    public bool hasState(Card cardPlayed, Card topCard, string postAction, int reward)
    {
        foreach (StateObj s in memory)
        {
            if (cardPlayed == s.getCardPlayed())
            {
                if (s.stateMatch(cardPlayed, topCard, postAction, reward))
                    return true;
            }
        }

        return false;
    }

    public bool hasAproxState(Card cardPlayed, Card topCard, string postAction, int reward)
    {
        foreach (StateObj s in memory)
        {
            if (s.reward > 0)
            {
                if (s.stateAproxMatch(cardPlayed, topCard, postAction) > 0)
                    return true;
            }
        }

        return false;
    }

    public Card findMatch()
    {
        foreach(Card c in p.getHand())
        {
            if (hasState(c, g.getDiscard().getTopCard(), "", 10))
            {
                return c;
            }
            else if(hasAproxState(c, g.getDiscard().getTopCard(), "", 10))
            {

            }

        }
        return null;
    }

    public void playRandomCard()
    {
        Card cardToPlay = p.getRandomCard();
        Card topCard = g.getDiscard().getTopCard();
        string outcome = GameActions.npcCheckCard(cardToPlay, p, g.getCurrentTurn(), g.getTurnDirection(), g.getDiscard(), g.displayText);
        int reward = setReward(p, outcome);
        addToMemory(cardToPlay, topCard, "", reward);
    }
}
    /*
    void example() {
        Player p = g.getKeith();
        while(g.gameRunning)
        {

            //agent.epsilon is set to give randomness to actions
            epsilon = 80 - turnCounter

            //get old state
            state_old = get_state(g, player1, food1)

            //perform random actions based on agent.epsilon, or  choose the action
            if (Random.Range(0, 1) < epsilon)
                final_move = to_categorical(randint(0, 2), num_classes = 3)
            else {
                //predict action based on the old state
                prediction = agent.model.predict(state_old.reshape((1, 11)))
                final_move = to_categorical(np.argmax(prediction[0]), num_classes = 3)[0]
            }
            //perform new move and get new state
            p.do_move(final_move, player1.x, player1.y, game, food1, agent)
            state_new = agent.get_state(game, player1, food1)
            //set treward for the new state
            reward = agent.set_reward(player1, game.crash)
        
            //train short memory base on the new action and state
            agent.train_short_memory(state_old, final_move, reward, state_new, game.crash)
        
            //store the new data into a long term memory
            agent.remember(state_old, final_move, reward, state_new, game.crash)
            record = get_record(game.score, record)
        }
    }*/
