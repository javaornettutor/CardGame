package model;

import model.interfaces.Player;

public class SimplePlayer implements Player {

	private String name, id;
	private int points, bet, result;

	// Added By Me
	public SimplePlayer(String id, String playerName, int initialPoints) {
		if (id == null || playerName == null)
			throw new IllegalArgumentException("Null Arguments");

		this.id = id;
		this.name = playerName;
		this.points = initialPoints;
	}

	@Override
	public String getPlayerName() {
		return this.name;
	}

	@Override
	public void setPlayerName(String playerName) {

		if (playerName != null) {
			this.name = playerName;
		}
	}

	@Override
	public int getPoints() {
		return this.points;
	}

	@Override
	public void setPoints(int points) {
		this.points = points;
	}

	@Override
	public String getPlayerId() {
		return this.id;
	}

	@Override
	public boolean setBet(int bet) {

		if (bet > 0 && points >= bet) {
			resetBet();
			this.bet = bet;
			return true;
		}
		return false;
	}

	@Override
	public int getBet() {
		return this.bet;
	}

	@Override
	public void resetBet() {
		bet = 0;
	}

	@Override
	public int getResult() {
		return this.result;
	}

	@Override
	public void setResult(int result) {
		if (result >= 0) {
			this.result = result;
		}
	}

	@Override
	public boolean equals(Player player) {
		return (this.id.equals(player.getPlayerId()));
	}

	@Override
	public boolean equals(Object obj) {

		if (obj instanceof Player) {
			Player player = (Player) obj;
			return (this.id.equals(player.getPlayerId()));
		}
		return false;
	}

	@Override
	public int hashCode() {
		return this.id.hashCode();
	}

	@Override
	public int compareTo(Player player) {
		return this.id.compareTo(player.getPlayerId());
	}

	@Override
	public String toString() {
		String str = "Player: ";

		str += "id = " + this.id + ",";
		str += "name = " + this.name + ",";
		str += "bet = " + this.bet + ",";
		str += "points = " + this.points + ",";
		str += "RESULT .. " + this.result + "\n";

		return str;
	}

}
