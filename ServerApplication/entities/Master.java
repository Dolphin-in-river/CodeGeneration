package com.example.test1.entities;

import java.util.*;

public class Master {
    private String name;
    private String birthday;
    private List<Cat> cats;
    private long id;

    public Master(String newName, String newBirthday) {
        this.name = newName;
        this.birthday = newBirthday;
        cats = new ArrayList<Cat>();
        this.id = (long) (Math.random() * 1000);
    }
    public String getName() {
        return this.name;
    }

    public String getBirthday() {
        return this.birthday;
    }
    public List<Cat> getCats() {
        return this.cats;
    }

    public long getId() {
        return this.id;
    }

    public void setNewName(String newName) {
        this.name = newName;
    }
    public void setCats(List<Cat> cats) {
        this.cats = cats;
    }

    public void setBirthday(String birthday) {
        this.birthday = birthday;
    }

    public void setId(long id) {
        this.id = id;
    }

    public void addCat(Cat newCat) {
        this.cats.add(newCat);
    }

    public void CreateMaster(Master master) {
        this.name = master.name;
        this.birthday = master.birthday;
        this.id = master.id;
        this.cats = master.cats;
    }

}