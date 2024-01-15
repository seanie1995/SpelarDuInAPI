

## Endpoints with their assigned payloads

### GET endpoints ###

  - /user/{userId}/track  **-Gets all tracks connected to a single user**

  - /user/{userId}/genre  **-Gets all genres connected to a single user**

  - /user/{userId}/artist  **-Gets all artists connected to a specific user**

  - /user/{userId}/track  **-Gets all tracks connected to a specific user**

    

    
### Post endpoints ###

  - /track  **-Add new track:**
    
  *Payload:*
  
```json
{  
    "TrackTitle": "Track title", 
    "Genre": "Genre",  
    "Artist": "Artist" 
}
```
- /user  **Adds new user**

*Payload:*

```json
{  
	"UserName": "User name"
}
```


![My Diagram](SpelarDuInAPIDiagram.drawio.svg)
