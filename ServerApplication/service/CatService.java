package com.example.test1.service;

import com.example.test1.entities.Cat;
import com.example.test1.entities.Master;
import org.springframework.stereotype.Service;

import java.util.*;

@Service
public class CatService implements ICatService {
    public List<Master> masters;

    public CatService() {
        masters = new ArrayList<>();
    }

    public List<Master> GetMasters() {
        return masters;
    }

    public Master CreateMaster(String name, String birthday) {
        var master = new Master(name, birthday);
        masters.add(master);
        return master;
    }

    public Cat GetCat(long id) {
        for (Master item : masters) {
            for (Cat itemCat : item.getCats()) {
                if (itemCat.getId() == id) {
                    return itemCat;
                }
            }
        }
        return null;
    }

    public Master AddMaster(Master master) {
        masters.add(master);
        return master;
    }

    public Cat CreateCat(Cat cat) {
        int counter = 0;
        for (Master item : masters) {
            if (item.getId() == cat.getMasterId()) {
                item.addCat(cat);
                counter++;
            }
        }
        if (counter == 0) {
            try {
                throw new Exception("Cat haven't found");
            } catch (Exception e) {
                e.printStackTrace();
            }
        }
        return cat;
    }

    public Cat AddCat(long idMaster, String name, String species, String birthday, String color) {
        var master = GetMaster(idMaster);
        var newCat = new Cat(name, species, birthday, color, idMaster);
        master.addCat(newCat);
        return newCat;
    }

    public Master GetMaster(long idMaster) {
        Master findMaster = null;
        for (Master master : masters) {
            if (idMaster == master.getId()) {
                findMaster = master;
            }
        }

        if (findMaster == null) {
            try {
                throw new Exception("Master haven't found");
            } catch (Exception e) {
                e.printStackTrace();
            }
        }
        return findMaster;
    }

    public List<Cat> GetCats() {
        var cats = new ArrayList<Cat>();
        for (int i = 0; i < masters.size(); i++) {
            var buffCats = masters.get(i).getCats();
            for (int j = 0; j < buffCats.size(); j++) {
                cats.add(buffCats.get(j));
            }
        }
        return cats;
    }
}
