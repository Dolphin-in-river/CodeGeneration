package com.example.test1.entities;

import java.util.ArrayList;
import java.util.List;


public class Cat {
    private String name;
    private String species;
    private String birthday;
    private String color;
    private long masterId;
    private List<Cat> friends;
    private long id;

    public Cat(String Name, String Species, String Birthday, String Color, long MasterId) {
        this.name = Name;
        this.species = Species;
        this.birthday = Birthday;
        this.color = Color;
        this.masterId = MasterId;
        friends = new ArrayList<Cat>();
        this.id = (long) (Math.random() * 1000);
    }

    public String getName() {
        return this.name;
    }

    public String getSpecies() {
        return this.species;
    }

    public String getBirthday() {
        return this.birthday;
    }

    public String getColor() {
        return this.color;
    }

    public String getDate() {
        return this.birthday;
    }

    public long getMasterId() {
        return this.masterId;
    }

    public List<Cat> getFriends() {
        return this.friends;
    }

    public long getId() {
        return this.id;
    }

    public void setName(String value) {
        this.name = value;
    }

    public void setSpecies(String value) {
        this.species = value;
    }

    public void setBirthday(String value) {
        this.birthday = value;
    }

    public void setColor(String value) {
        this.color = value;
    }

    public void setDate(String value) {
        this.birthday = value;
    }

    public void setMasterId(long value) {
        this.masterId = value;
    }

    public void setFriends(List<Cat> value) {
        this.friends = value;
    }

    public void setId(long value) {
        this.id = value;
    }


    public void addFriend(Cat newCat) {
        this.friends.add(newCat);
    }
}

