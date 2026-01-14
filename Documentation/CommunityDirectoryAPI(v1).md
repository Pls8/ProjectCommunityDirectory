# Community Directory API

**Version:** v1
**Specification:** OpenAPI 3.0
**Base URL:** `https://localhost:7174/api`

---

## Authentication

The API uses **JWT Bearer Token authentication**.

### Steps

1. Register or log in using the Account endpoints.
2. Copy the returned `token` from the response.
3. In Postman:

   * Authorization Type: **Bearer Token**
   * Token: `<JWT_TOKEN>`

---

## Account

### Register

**POST** `/account/register`

**Request Body**

```json
{
  "fullName": "Postman",
  "email": "Postman@example.com",
  "password": "Postman_123",
  "confirmPassword": "Postman_123"
}
```

**Response**

```json
{
  "success": true,
  "message": "Registration successful",
  "token": "jwt_token",
  "expiration": "2026-01-01T00:00:00Z",
  "user": {
    "id": "465f6a39-2807-45c3-b9fa-4a3263ae7fa1",
    "email": "Postman@example.com",
    "fullName": "Postman",
    "roles": ["User"]
  }
}
```

---

### Login

**POST** `/account/login`

**Request Body**

```json
{
  "email": "Postman@example.com",
  "password": "Postman_123"
}
```

---

## Categories

### Get All Categories

**GET** `/categories`

**Response**

```json
[
  {
    "id": 1,
    "name": "Health",
    "description": "Health-related services"
  }
]
```

---

### Create Category

**POST** `/categories`

**Request Body**

```json
{
  "name": "Education",
  "description": "Educational resources"
}
```

---

### Get Category by ID

**GET** `/categories/{id}`

**Path Parameter**

* `id` (integer)

---

### Update Category

**PUT** `/categories/{id}`

**Request Body**

```json
{
  "name": "Updated Name",
  "description": "Updated description"
}
```

---

### Delete Category

**DELETE** `/categories/{id}`

---

## Events

### Get All Events

**GET** `/events`

**Response**

```json
[
  {
    "id": 1,
    "title": "Community Meetup",
    "description": "Monthly meetup",
    "eventDate": "2026-02-15",
    "location": "City Hall",
    "organizer": "Community Org",
    "imagePath": "image.jpg",
    "categoryName": "Social",
    "categoryId": 2
  }
]
```

---

### Create Event

**POST** `/events`

**Request Body**

```json
{
  "title": "Workshop",
  "description": "Skill-building workshop",
  "eventDate": "2026-03-01",
  "location": "Library",
  "organizer": "Tech Group",
  "categoryId": 3
}
```

---

### Get Event by ID

**GET** `/events/{id}`

---

### Delete Event

**DELETE** `/events/{id}`

---

## Resources

### Get All Resources

**GET** `/resources`

---

### Create Resource

**POST** `/resources`

**Request Body**

```json
{
  "name": "Food Bank",
  "description": "Free food assistance",
  "categoryId": 1,
  "phone": "123-456-7890",
  "contactEmail": "info@foodbank.org",
  "contactInfo": "Call during office hours",
  "city": "New York",
  "address": "123 Main St",
  "website": "https://foodbank.org"
}
```

---

### Search Resources

**GET** `/resources/search`

**Query Parameters** (optional)

* `name`
* `categoryId`
* `city`

---

### Get Resource by ID

**GET** `/resources/{id}`

---

### Delete Resource

**DELETE** `/resources/{id}`

---

### Approve Resource

**PUT** `/resources/approve/{id}`

Marks a resource as approved. Typically restricted to admin roles.

---

## Data Models

### UserDto

```json
{
  "id": 1,
  "email": "john@example.com",
  "fullName": "John Doe",
  "roles": ["User"]
}
```

### CategoryDto

```json
{
  "id": 1,
  "name": "Health",
  "description": "Health services"
}
```

### EventDto

```json
{
  "id": 1,
  "title": "Community Event",
  "description": "Event description",
  "eventDate": "2026-01-20",
  "location": "Community Center",
  "organizer": "Organizer Name",
  "imagePath": "event.jpg",
  "categoryName": "Social",
  "categoryId": 2
}
```

### ResourceCreateDto

```json
{
  "name": "Resource Name",
  "description": "Description",
  "categoryId": 1,
  "phone": "123456789",
  "contactEmail": "contact@email.com",
  "contactInfo": "Additional info",
  "city": "City",
  "address": "Address",
  "website": "https://example.com",
  "isApproved": false
}
```


