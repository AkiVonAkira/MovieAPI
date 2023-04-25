# MovieAPI

### An ASP .NET Core Mininimal Web API for movies utulizing code first SQL database.

---

## ⚙️ Technologies Used

![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white)
![MicrosoftSQLServer](https://img.shields.io/badge/Microsoft%20SQL%20Server-CC2927?style=for-the-badge&logo=microsoft%20sql%20server&logoColor=white)
![Minimal](https://img.shields.io/badge/Minimal_API-005571?style=for-the-badge&logo=.net)

---

## 🔑 Key features

- Create, and link movies, genres and persons
- Discover movies using TMDB.

---

## 📞 API Calls

### GET

Returns all movies, genres or persons in the system.

> `​/api​/movie`
>
> `​/api​/genre​/`
>
> `​/api​/person​/`

Search for movie by its ID.

> `​/api​/movie​/{id}`

Get movies/genres linked to a person or ratings linked to a person.

Parameters: `name`

> `​/api​/person​/movie`
>
> `​/api​/person/genre`
>
> `​/api​/rating​/person`

Discover movies with genre name using TMDB.

Parameters: `genreName`

> `​/api​/movie​/discover​/`

---

### POST

Creates movies, genres or persons in the system.

> `​/api​/movie`
>
> `​/api​/genre`
>
> `​/api​/person`

Link movies/genres to a person.

> `​/api​/person​/movie`
>
> `​/api​/person​/genre`

Add rating to a movie as a person.

> `​/api​/rating​/person`

Create a movie as a person and a initial genre

Parameters: `personId`, `genreName`

> `​/api​/person​/MovieLinks`

Example: `/api/person/MovieLinks?personId=2&genreName=Adventure`

---

### DELETE

Delete movie with an ID

`​/api​/movie​/{id}`

---

### PUT

Update movie with an ID (body requires everything but the ID)

`​/api​/movie​/{id}`

---
