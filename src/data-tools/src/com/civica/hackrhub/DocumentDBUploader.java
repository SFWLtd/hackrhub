package com.civica.hackrhub;

import com.google.gson.Gson;
import com.microsoft.azure.documentdb.*;

import java.util.List;

public class DocumentDBUploader {
    private static final String HOST = "https://civica-hackathon.documents.azure.com:443/";
    private static final String MASTER_KEY = "7XqRxFV1lWMWp0SL9AzBfsUiFRXLKfhASuKRW2s9b3IBM2Zvf3hXxeRjqvhI0WPuFxLZE1bN22GhtnT4ZuRU8g==";

    private static final String DATABASE_ID = "ToDoList";

    // The name of our collection.
    private static final String COLLECTION_ID = "Items";

    private static DocumentClient documentClient;


    // Cache for the database object, so we don't have to query for it to
    // retrieve self links.
    private static Database databaseCache;

    // We'll use Gson for POJO <=> JSON serialization for this example.
    private static Gson gson = new Gson();

    // Cache for the collection object, so we don't have to query for it to
    // retrieve self links.
    private static DocumentCollection collectionCache;

    public static DocumentClient getDocumentClient() {
        if (documentClient == null) {
            documentClient = new DocumentClient(HOST, MASTER_KEY,
                    ConnectionPolicy.GetDefault(), ConsistencyLevel.Session);
        }

        return documentClient;
    }

    public static void main(String[] args) throws DocumentClientException {
        /*
        Left4Dev - One support system to rule them all. .Net Core, Azue / Linux
Youse’uns Want Lunch - Team lunch organisation helper. Django, Python, Slack integration
Google's Home Boyz! - Google voice integration to GitHub. .Net, MVC, Azure
Coding For Food - Quiz engine with integration to open source / custom db for Qs.  Python, Django, Angular2
ZZ Bottom - Android App for integrating interesting things in Bath.  Java, AWS, #serverless, Lambda
         */

        TeamItem[] teams={
                new TeamItem("Observers","0","unknown","None","None"),
                new TeamItem("Left4Dev","1","Belfast","GitGud","One support system to rule them all. .Net Core, Azue / Linux"),
                new TeamItem("Youse’uns Want Lunch","2","Belfast","Lunch Bots","Team lunch organisation helper. Django, Python, Slack integration"),
                new TeamItem("Google's Home Boyz!","3","Belfast","Source Helper","Google voice integration to GitHub. .Net, MVC, Azure"),
                new TeamItem("Coding For Food","4","Belfast","Quiz Monster","Quiz engine with integration to open source / custom db for Qs.  Python, Django, Angular2"),
                new TeamItem("Toni&Guys","5","Woking","HackRHub","Platform for running Hackathons with AI login, Aurelia front end, DocumentDB, .Net Web API"),
                new TeamItem("ZZ Bottom","6","Bath","WhatsMap","Android App for integrating interesting things in Bath.  Java, AWS, #serverless, Lambda"),
        };

        UserItem[] users={
                new UserItem("2de0f9a5-17e0-41fb-9aef-f2d86f99906d","Alistair Massey","Executive Software Engineer ","0"),
                new UserItem("22496477-8033-4199-82ce-0ea3b46a1c55","Andrew Fletcher","Software Engineering Scholarship Student ","1"),
                new UserItem("a6abc08c-aeaf-4393-9b68-4a60e8294808","Bruce Mundin","Architect","0"),
                new UserItem("7fbe0c8b-73d0-4099-8cb9-1a84eafdbe7b","Cathan O'Donnell","Graduate Engineer","3"),
                new UserItem("27d59f42-ed9e-4c93-ac96-694b0d257fd0","Christopher Kane","Engineer","4"),
                new UserItem("2c8431d7-6591-4679-b64c-e185606a6e97","Ciaran Gallagher","Software Engineer","1"),
                new UserItem("50b91823-96c3-4dcc-9615-fe6de627c985","Ciaran Mulholland","Placement Software Engineer","1"),
                new UserItem("54e2724f-daa5-45a2-a017-36a93b009653","Colin Campbell","Senior Software Engineer","3"),
                new UserItem("abdee4de-69f0-43f2-879f-23125dcfd94d","Dan Barclay","Graduate Software Engineer","6"),
                new UserItem("1780f268-3043-41d2-972d-d565460ae64c","David Raine","Solutions Architect","5"),
                new UserItem("70411582-1043-4487-904d-f4a697e3d91b","Ed Ashford","Graduate Software Engineer","6"),
                new UserItem("d275a308-7457-46c5-9599-4c5bf422bce4","Emily Lawes","Graduate Software Engineer","6"),
                new UserItem("ce9caf0e-788d-4129-94d5-3ac03f027fc3","Gavin Harte","Technology & Solutions Director ","0"),
                new UserItem("e6524bee-fdc3-4e5d-ad34-fa16ac0fa2f7","Laura Soutar","Support Engineer","2"),
                new UserItem("63ac61e1-d5fd-46dc-bef0-b5cf587ef68d","Matt George","Applications Specialist ","5"),
                new UserItem("5c71eafb-37fe-49c3-913b-b272c3001dc6","Michael Ankrett","Analyst/Developer","6"),
                new UserItem("c7d8db72-7b16-4db2-8aa4-9547040320c7","Michael Purdy","Placement Software Engineer","4"),
                new UserItem("ec2abebe-833b-496e-ac9e-34dfb73ebaa0","Michael Seabrook","Principal Developer","3"),
                new UserItem("c798861f-4efe-41d9-ae1c-57c8380cfd24","Niall Mckeown","Placement Software Engineer","1"),
                new UserItem("f2a69a53-4cff-4228-ba92-93f84d18a5e8","Paul Crooks","Senior Software Engineer","3"),
                new UserItem("f197af1f-514d-4214-99cd-41ba85039ae4","Richard Press","Principal Project Manager","0"),
                new UserItem("5ddc2645-f668-4045-b516-319c7f0321ad","Rick Powell","Analyst Programmer","5"),
                new UserItem("42bb3814-b423-429c-86d8-c63529e51464","Ryan Warren","Ryan Warren","2"),
                new UserItem("40f05c68-e855-4134-b17f-af71bd2ae7e5","Sam Jenkins","Analyst/Developer","6"),
                new UserItem("173dfb85-66e6-4226-b862-d3ca2916366e","Scott Culcheth","Lead Developer","2"),
                new UserItem("244c3a86-ceaf-4322-b288-569d121b8b4c","Scott Hulme","Senior Software Engineer","4"),
                new UserItem("26e2134d-7484-4b95-adfe-a27166e580e1","Sean Carlin","Senior Engineer","3"),
                new UserItem("bdda7f39-b916-4709-9f94-f65317c1e577","Simon Kelly","Full Stack Developer","5"),
                new UserItem("0598c6a9-cd54-4e33-a154-54d477f88a19","Simon Wallace","Graduate Software Engineer","2"),
                new UserItem("9885de5d-df93-48fa-a04d-8aa86ae29cf4","Toni Kim","Senior Interaction Designer ","5"),
                new UserItem("c83eb71d-88ec-4f6b-87ed-235f1578ded5","Victoria McCallum","ICT Apprentice Software Engineer","2"),

        };
        DocumentClient client = getDocumentClient();

        List<DocumentCollection> c = getCollections();
        if(c.size()>0){
            client.deleteCollection(c.get(0).getSelfLink(),null);
        }


        for(TeamItem team: teams){
            Document teamDoc = new Document(gson.toJson(team));
            System.out.println(gson.toJson(team));
            try {
                teamDoc.set("entityType", "team");
                teamDoc = documentClient.createDocument(
                        getUserCollection().getSelfLink(), teamDoc, null,
                        false).getResource();
            }catch (Exception e){e.printStackTrace();}

        }
        for(UserItem user: users){
            Document userDoc = new Document(gson.toJson(user));
            System.out.println(gson.toJson(user));
            try{
                userDoc.set("entityType", "user");
            userDoc =documentClient.createDocument(
                    getUserCollection().getSelfLink(), userDoc, null,
                    false).getResource();
            }catch (Exception e){e.printStackTrace();}
        }


    }

    private static List<DocumentCollection> getCollections(){
        return documentClient
                .queryCollections(
                        getUserDatabase().getSelfLink(),
                        "SELECT * FROM root r WHERE r.id='" + COLLECTION_ID
                                + "'", null).getQueryIterable().toList();
    }

    private static DocumentCollection getUserCollection() {
        if (collectionCache == null) {
            // Get the collection if it exists.
            List<DocumentCollection> collectionList = getCollections();

            if (collectionList.size() > 0) {
                // Cache the collection object so we won't have to query for it
                // later to retrieve the selfLink.
                collectionCache = collectionList.get(0);
            } else {
                // Create the collection if it doesn't exist.
                try {
                    DocumentCollection collectionDefinition = new DocumentCollection();
                    collectionDefinition.setId(COLLECTION_ID);

                    collectionCache = documentClient.createCollection(
                            getUserDatabase().getSelfLink(),
                            collectionDefinition, null).getResource();
                } catch (DocumentClientException e) {
                    // TODO: Something has gone terribly wrong - the app wasn't
                    // able to query or create the collection.
                    // Verify your connection, endpoint, and key.
                    e.printStackTrace();
                }
            }
        }

        return collectionCache;
    }

    private static Database getUserDatabase() {
        if (databaseCache == null) {
            // Get the database if it exists
            List<Database> databaseList = documentClient
                    .queryDatabases(
                            "SELECT * FROM root r WHERE r.id='" + DATABASE_ID
                                    + "'", null).getQueryIterable().toList();

            if (databaseList.size() > 0) {
                // Cache the database object so we won't have to query for it
                // later to retrieve the selfLink.
                databaseCache = databaseList.get(0);
            } else {
                // Create the database if it doesn't exist.
                try {
                    Database databaseDefinition = new Database();
                    databaseDefinition.setId(DATABASE_ID);

                    databaseCache = documentClient.createDatabase(
                            databaseDefinition, null).getResource();
                } catch (DocumentClientException e) {
                    // TODO: Something has gone terribly wrong - the app wasn't
                    // able to query or create the collection.
                    // Verify your connection, endpoint, and key.
                    e.printStackTrace();
                }
            }
        }

        return databaseCache;
    }
}
