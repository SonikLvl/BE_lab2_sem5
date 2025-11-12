# Лабораторна 4

Мета роботи: Додати можливість аутентифікації в проект

Виконала студентка групи ІО-32 Куліченко Софія

## Зміст

- [Features](#features)
- [TechStack](#techstack)
- [Локальний запуск](#локальний-запуск)
- [Deploy](#deploy)
- [Usage](#usage)
- [Endpoints API](#endpoints-api)

---

## Features 

API задеплоєно на Render.com. Ви можете переглянути всю документацію та протестувати ендпоінти в реальному часі за посиланням на Swagger UI:

https://be-lab4-sem5.onrender.com/index.html

---

## TechStack

* **.NET 8** 
* **ASP.NET Core**
* **PostgreSQL** 
* **Swashbuckle (Swagger)** 

---

## Локальний запуск

Для того, щоб запустити цей проєкт локально, виконайте наступні кроки.

### **Передумови**

Переконайтесь, що у вас встановлено наступне програмне забезпечення:

1.  **[.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)**
2.  **[Git](https://git-scm.com/)**
3.  **[PostgreSQL Server](https://www.postgresql.org/download/)**
4.  **Будь-який клієнт для керування БД (наприклад, [pgAdmin](https://www.pgadmin.org/) або [DBeaver](https://dbeaver.io/))**
5.  **[.NET user-secrets інструмент](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-9.0&tabs=windows)**

### **Інструкції для запуску**

1.  **Клонуйте репозиторій:**
    Відкрийте термінал або командний рядок і виконайте команду:
    ```bash
    git clone https://github.com/SonikLvl/BE_lab2_sem5.git
    ```

2.  **Налаштування Бази Даних (PostgreSQL)**
    Відкрийте pgAdmin або інший клієнт баз даних.
    Створіть нову, порожню базу даних для проекту.

3.  **Налаштування Конфігурації (User Secrets)**
  
    Переконайтеся, що ви знаходитесь у директорії проекту (там, де лежить .csproj файл).
    Ініціалізуйте user secrets:
    ```bash
    dotnet user-secrets init
    ```
    Встановіть рядок підключення (Connection String).
    ```bash
    dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Database=finance_db_local;Username=ВАШ_USERNAME;Password=ВАШ_ПАРОЛЬ"
    ```

    Підготуйте JWT. Для цього виконайте ці команди:
    ```bash
    $rng = [System.Security.Cryptography.RandomNumberGenerator]::Create()
    $rng.GetBytes($bytes)
    [Convert]::ToBase64String($bytes)
    ```
    
    Скопіюйте виданий рядок та додайте до usersecrets:
    ```bash
    dotnet user-secrets set "Jwt:Key" "Рядок_з_попереднього_кроку"
    ```

5.  **Застосування міграцій та запуск**
    Додайте міграцію 
    ```bash
    dotnet ef migrations add [MigrationName]
    ```
    Актуалізуйте БД
    ```bash
    dotnet ef database update
    ```
    Запустіть проект:
    ```bash
    dotnet run
    ```

    АБО

    Запустіть проект за допомогою докер. Але usersecrets не працюватиме потрібно буде визначати їх в docker файлі.
    ```bash
    docker-compose up --build
    ```
    
    
4.  **Перевірка**
    API буде запущено на локальному хості (наприклад, http://localhost:5123 або https://localhost:8080 (для докеру)). Точну адресу ви побачите у консолі.
    За цим посиланням буде відкрито Swagger UI.
    
---

## Deploy

Якщо немає бажання підіймати АПІ локально, то проект задеплоєний за посиланням [https://be-lab4-sem5.onrender.com/](https://be-lab4-sem5.onrender.com/index.html).

---

## Usage

Ви можете тестувати API за допомогою будь-якого клієнта, наприклад, **[Postman](https://www.postman.com/)**.

Базова URL-адреса для запитів: `https://localhost:<PORT_NUMBER>`

### **Приклад тестування в Postman**

Для зручного тестування ви можете використати наданий **Postman Flow**.

<img width="1997" height="1176" alt="Screenshot 2025-11-12 103508" src="https://github.com/user-attachments/assets/f0a2e450-6e0b-4947-9466-b6ef355f40f0" />

---

## Endpoints API

Нижче наведено список доступних ендпоінтів.
Всі окрім register та login у юзера потребують JWT токена.

Http запит повинен починатись з {{baseURL}}/api/....

| Метод  | Шлях                                            | Опис                                                                                             |
| :----- | :---------------------------------------------- | :-------------------------------------------------------------------------------------------- |
 | User 
| `POST` | `/user/register`                                        | Створює нового користувача.                                                         |
| `POST` | `/user/login`                                        | Створює токен авторизації користувача.                                                         |
| `GET`  | `/user/users`                                  | Отримує список усіх користувачів.                                                     |
| `GET`  | `/user/{id}`                                   | Отримує користувача за його `id`.                                                       |
| `GET`  | `/user/self`                                   | Отримує користувача за токеном авторизації.                                                       |
| `DELETE`| `/user/{id}`                                   | Видаляє користувача за його `id`.                                                   |
| `DELETE`| `/user/self`                                   | Видаляє користувача за токеном авторизації.                                                  |
|  Category 
| `POST`  | `/category`                 | Створює нову категорію для юзера (відсутній юзер рівняється публічній категорії).     |
| `GET`  | `/category/all`                 | Отримує список усіх категорій.                |
| `GET`  | `/category/allById`                 | Отримує список усіх категорій для юзера (включно з публічними).                |
| `GET`  | `/category/{id}`                 | Отримує категорію за юзером (включно з публічними).             |
| `GET`  | `/category?categoryName={category_name}`                 | Отримує категорію за юзером та ім'ям (включно з публічними).               |
| `DELETE`| `/category/{id}`                    | Видаляє категорію за її `id`                           |
|  Record 
| `POST` | `/record`                                       | Створює новий запис про витрати.                                                         |
| `GET`  | `/record/{id}`                                  | Отримує запис за його `id`.                                                            |
| `DELETE`| `/record/{id}`                | Видаляє запис за його `id`.                     |
| `GET`  | `/record?categoryId={id}`    | Отримує записи, відфільтровані за `userId` та/або `categoryId`. |

---
