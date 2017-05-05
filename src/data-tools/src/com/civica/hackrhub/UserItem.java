package com.civica.hackrhub;

/**
 * Created by SKX on 04/05/2017.
 */
public class UserItem {
    public UserItem(String personId, String name, String title, String teamId){
        this.personId = personId;
        this.name=name;
        this.title=title;
        this.teamId=teamId;
    }

    String personId;
    String name;
    String title;
    String teamId;
}
