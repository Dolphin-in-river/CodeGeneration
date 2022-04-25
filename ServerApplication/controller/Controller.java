package com.example.test1.controller;

import com.example.test1.entities.Cat;
import com.example.test1.entities.Master;
import com.example.test1.service.CatService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.ArrayList;
import java.util.List;


@RestController
public class Controller {

    @Autowired
    public CatService _catService = new CatService();

    @GetMapping(value = "/masters")
    public ResponseEntity<List<Master>> GetMasters(){
        final List<Master> masters = _catService.GetMasters();
        return new ResponseEntity<>(masters, HttpStatus.OK);
    }

    @GetMapping(path = "/getMaster/{id}", produces = MediaType.APPLICATION_JSON_VALUE)
    public ResponseEntity<Master> GetMaster(@PathVariable long id){
        return new ResponseEntity<>(_catService.GetMaster(id), HttpStatus.OK);
    }

    @PostMapping(path = "/createMaster")
    public ResponseEntity<?> CreateMaster(@RequestBody Master newMaster){
        Master master = new Master(newMaster.getName(), newMaster.getBirthday());
        _catService.AddMaster(master);
        return new ResponseEntity<>(master.getId(), HttpStatus.OK);
    }


    @GetMapping(path = "/getCat/{id}", produces = MediaType.APPLICATION_JSON_VALUE)
    public ResponseEntity<Cat> GetCat(@PathVariable long id){
        final Cat cat = _catService.GetCat(id);
        return cat != null
                ? new ResponseEntity<>(cat, HttpStatus.OK)
                : new ResponseEntity<>(HttpStatus.NOT_FOUND);
    }

    @PostMapping(path = "/createCat")
    public ResponseEntity<?> CreateCat(@RequestBody Cat cat){
        Cat newCat = new Cat(cat.getName(), cat.getSpecies(), cat.getBirthday(), cat.getColor(), cat.getMasterId());
        _catService.CreateCat(newCat);
        return new ResponseEntity<>(newCat.getId(), HttpStatus.OK);
    }
    @PostMapping(path = "/setFriends/{id}")
    public ResponseEntity<?> SetFriends(@RequestBody List<Cat> cats, @PathVariable long id){
        var cat = _catService.GetCat(id);
        var newCats = new ArrayList<Cat>();
        for(int i = 0; i < cats.size(); i++){
            var buffCat = new Cat(cats.get(i).getName(), cats.get(i).getSpecies(), cats.get(i).getBirthday(), cats.get(i).getColor(), cats.get(i).getMasterId());
            newCats.add(buffCat);
        }
        cat.setFriends(newCats);
        return new ResponseEntity<>(cat.getFriends(), HttpStatus.OK);
    }
}


