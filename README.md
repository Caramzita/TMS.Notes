# Сервис управления заметками

Этот сервис предоставляет REST API для управления заметками. Он позволяет создавать, читать, обновлять и удалять заметки.

## Endpoints

### Get All Notes

- **Endpoint:** `GET /api/Note`
- **Description:** Retrieves a list of all notes.

### Create a Note

- **Endpoint:** `POST /api/Note`
- **Description:** Creates a new note.
- **Request Body:**
  ```json
  {
    "title": "test title",
    "description": "test description"
  }
- **Response**:
  - `201 Created if successful`
  - `400 Bad Request if there are validation errors`

### Update a Note
- **Endpoint:** `PUT /api/Note`
- **Description:** Updates an existing note.
- **Request Body:**
  ```json
  {
    "id": "b79f008f-451f-4ce1-ad31-a948ddefd3d4",
    "title": "test title2",
    "description": "test description2"
  }
 - **Response**: 
   - `200 OK if successful`
   - `400 Bad Request if there are validation errors`
   - `404 Not Found if the note does not exist`
### Delete a Note
- **Endpoint:** `DELETE /api/Note/{id}`
- **Description:** Deletes a note by its ID.
- **Request URL Parameter:**
    - `id: The ID of the note to delete`
- **Response**:
   - `204 No Content if successful`
   - `404 Not Found if the note does not exist`
 
## Configuration
### Example Configuration
  ```json
  {
    "ConsulConfig": {
      "Address": "http://host.docker.internal:8500",
      "ServiceName": "Notes Service",
      "ServiceAddress": "localhost",
      "ServicePort": 8081,
      "Tags": [ "api", "notes" ]
    },

    "ConsulKey": "JwtTokenSettings",

    "JwtTokenSettings": {
      "SecretKey": "K17T6p+mYlBuIll6EOQDUmAdM6xmzeHOpE+O35zsAvw=",
      "Issuer": "TMS.Serurity.Service",
      "Audience": "TMS.Notes.Service",
      "AccessTokenLifetimeInMinutes": 15,
      "RefreshTokenLifetimeInMinutes": 7
    },

    "DatabaseConnection": "Server=postgres;Port=5432;Database=db;User Id=postgres;Password=admin;"
  }
