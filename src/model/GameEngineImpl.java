package model;

import java.util.ArrayDeque;
import java.util.ArrayList;
import java.util.Collection;
import java.util.Collections;
import java.util.Deque;
import java.util.List;

import model.interfaces.GameEngine;
import model.interfaces.Player;
import model.interfaces.PlayingCard;
import model.interfaces.PlayingCard.Suit;
import view.interfaces.GameEngineCallback;

public class GameEngineImpl implements GameEngine {

	private Collection<Player> playerList;

	private List<PlayingCard> cards;

	private Collection<GameEngineCallback> gameEngineCallbackList;

	// Default Constructor
	public GameEngineImpl() {
		playerList = new ArrayList<>();
		gameEngineCallbackList = new ArrayList<GameEngineCallback>();
		initializeCards();
	}

	private void initializeCards() {
		cards = new ArrayList<>();
		for (Suit suit : Suit.values()) {
			for (PlayingCard.Value value : PlayingCard.Value.values()) {
				cards.add(new PlayingCardImpl(suit, value));
			}
		}
	}

	@Override
	public void dealPlayer(Player player, int delay) throws IllegalArgumentException {
		ensureValidDelay(delay);
		 
		Deque<PlayingCard> cards = getShuffledHalfDeck();
		int score = 0;
		PlayingCard nextCard = cards.removeFirst();
				
		while(score + nextCard.getScore() < BUST_LEVEL) {
			score += nextCard.getScore();
			
			for(GameEngineCallback gameEngineCallback : gameEngineCallbackList) {
				gameEngineCallback.nextCard(player, nextCard, this);
			}
			
			nextCard = cards.removeFirst();
			
			waitFor(delay);
		};
		
		for(GameEngineCallback gameEngineCallback : gameEngineCallbackList) {
			gameEngineCallback.bustCard(player, nextCard, this);
			gameEngineCallback.result(player, score, this);
		}
	
		player.setResult(score);
	}

	@Override
	public void dealHouse(int delay) throws IllegalArgumentException {
		ensureValidDelay(delay);
		
		Deque<PlayingCard> cards = getShuffledHalfDeck();
		int score = 0;
		PlayingCard nextCard = cards.removeFirst();
				
		while(score + nextCard.getScore() < BUST_LEVEL) {
			score += nextCard.getScore();
			
			for(GameEngineCallback gameEngineCallback : gameEngineCallbackList) {
				gameEngineCallback.nextHouseCard(nextCard, this);
			}
			nextCard = cards.removeFirst();
			
			waitFor(delay);
		};
		
		for (Player player : playerList) {
			applyWinLoss(player, score);
		}
		
		for(GameEngineCallback gameEngineCallback : gameEngineCallbackList) {
			gameEngineCallback.houseBustCard(nextCard, this);
		}
		
		for (Player player : playerList) {
			player.resetBet();
		}
	}

	@Override
	public void applyWinLoss(Player player, int houseResult) {
		if (player.getResult() > houseResult) {
			player.setPoints(player.getPoints() + player.getBet());
		} else if (player.getResult() < houseResult) {
			player.setPoints(player.getPoints() - player.getBet());
		}
	}

	@Override
	public void addPlayer(Player player) {
		playerList.add(player);
	}

	@Override
	public Player getPlayer(String id) {
		for (Player player : playerList) {
			if (player.getPlayerId().equalsIgnoreCase(id)) {
				return player;
			}
		}
		return null;
	}

	@Override
	public boolean removePlayer(Player player) {
		playerList.remove(player);
		return false;
	}

	@Override
	public boolean placeBet(Player player, int bet) {
		return player.setBet(bet);
	}

	@Override
	public void addGameEngineCallback(GameEngineCallback gameEngineCallback) {
		gameEngineCallbackList.add(gameEngineCallback);
	}

	@Override
	public boolean removeGameEngineCallback(GameEngineCallback gameEngineCallback) {

		return gameEngineCallbackList.remove(gameEngineCallback);
	}

	@Override
	public Collection<Player> getAllPlayers() {
		return playerList;
	}

	@Override
	public Deque<PlayingCard> getShuffledHalfDeck() {
		Collections.shuffle(cards);
		return new ArrayDeque<PlayingCard>(cards);
	}
	
	private void ensureValidDelay(int delay) throws IllegalArgumentException {
		if (delay < 0 && delay > 1000) {
			throw new IllegalArgumentException();
		}
	}
	
	private void waitFor(int delay) {
		try        
		{
		    Thread.sleep(delay);
		} 
		catch(InterruptedException ex) 
		{
		    Thread.currentThread().interrupt();
		}
	}
}
