package com.civica.hackrhub;

import java.util.List;

/**
 * Created by SKX on 04/05/2017.
 */
public class TeamItem {
    private String id;
    private String name;
    private List<UserItem> users;
    private String location;
    private String projectName;
    private String projectDescription;


    public String getId() {
        return id;
    }

    public void setId(String id) {
        this.id = id;
    }

    public TeamItem(String name, String id,String location,String projectName,String projectDescription){
        this.name=name;
        this.id=id;
        this.location=location;
        this.projectName=projectName;
        this.projectDescription=projectDescription;
    }
}
