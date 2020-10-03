package model;

import model.interfaces.PlayingCard;

public class PlayingCardImpl implements PlayingCard {

	// Variables
	static final int DECK_SIZE = 28;
	public Suit suit;
	public Value value;

	// Default Constructor
	public PlayingCardImpl() {
		super();
	}

	// Constructor with Parameters
	public PlayingCardImpl(Suit suit, Value value) {
		if (suit == null || value == null)
			throw new IllegalArgumentException("Null Arguments");

		this.suit = suit;
		this.value = value;

	}

	@Override
	public Suit getSuit() {
		return this.suit;
	}

	@Override
	public Value getValue() {
		return this.value;
	}

	@Override
	public int getScore() {
		
		switch (this.value) {
		case ACE:
			return 11;
		case EIGHT:
			return 8;
		case NINE:
			return 9;
		case JACK:
		case KING:
		case QUEEN:
		case TEN:
		default:
			return 10;
		}
	}

	@Override
	public String toString() {
		String str;

		str = "Suit: " + this.getSuit().name() + ",";
		str += "Value: " + this.getValue().name() + ",";
		str += "Score: " + this.getScore();

		return str;
	}

	@Override
	public boolean equals(PlayingCard card) {
		return (this.value.equals(card.getValue()) && this.suit.equals(card.getSuit()));
	}

	@Override
	public boolean equals(Object obj) {

		if (obj instanceof PlayingCard) {
			PlayingCard card = (PlayingCard) obj;
			return (this.value.equals(card.getValue()) && this.suit.equals(card.getSuit()));
		}
		return false;
	}

	@Override
	public int hashCode() {
		return this.suit.hashCode() + this.value.hashCode();
	}

}
