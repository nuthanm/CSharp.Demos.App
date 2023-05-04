using Microsoft.EntityFrameworkCore;
using TodoApi;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TodoDb>(options => options.UseInMemoryDatabase("TodoList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
var app = builder.Build();


// Initial default routing code
//app.MapGet("/", () => "Hello World!");

// Changing to project specific routings

//// Get all todoitems
//app.MapGet("/todoitems", async (TodoDb db) =>
//          await db.Todos.ToListAsync());

//// Get only completed items
//app.MapGet("/todoitems/complete", async (TodoDb db) =>
//          await db.Todos.Where(x => x.IsComplete).ToListAsync());


//// Get requested item
//app.MapGet("/todoitems/{id}", async (int id, TodoDb db) =>
//           await db.Todos.FindAsync(id)
//           is Todo todo
//            ? Results.Ok(todo)
//            : Results.NotFound()
//);

//// Create a new todo item
//app.MapPost("/todoitems", async (Todo todo, TodoDb db) =>
//    {
//        db.Todos.Add(todo);
//        await db.SaveChangesAsync();

//        Results.Created($"/todoitems/{todo.Id}", todo);
//    });

//// Update an existing item
//app.MapPut("/todoitems/{id}", async (int id, Todo inTodo, TodoDb db) =>
//{
//    var todo = await db.Todos.FindAsync(id);
//    if (todo is null)
//    {
//        return Results.NotFound();
//    }

//    todo.Name = inTodo.Name;
//    todo.IsComplete = inTodo.IsComplete;

//    await db.SaveChangesAsync();
//    return Results.NoContent();
//});

//// Delete a requested one

//app.MapDelete("/todoitems/{id}", async (int id, TodoDb db) =>
//{
//    var todo = await db.Todos.FindAsync(id);
//    if(todo is null)
//    {
//        return Results.NotFound();
//    }

//    db.Todos.Remove(todo);
//    await db.SaveChangesAsync();

//    return Results.Ok(todo);
//});

//// To save repitative endpoint there is a concept called MapGroup which keeps this "/todoitems" in one place and resuse it everywhere

//var todoItems = app.MapGroup("/todoitems");

//// Get all todoitems
//todoItems.MapGet("/", async (TodoDb db) =>
//          await db.Todos.ToListAsync());

//// Get only completed items
//todoItems.MapGet("/complete", async (TodoDb db) =>
//          await db.Todos.Where(x => x.IsComplete).ToListAsync());


//// Get requested item
//todoItems.MapGet("/{id}", async (int id, TodoDb db) =>
//           await db.Todos.FindAsync(id)
//           is Todo todo
//            ? Results.Ok(todo)
//            : Results.NotFound()
//);

//// Create a new todo item
//todoItems.MapPost("/", async (Todo todo, TodoDb db) =>
//{
//    db.Todos.Add(todo);
//    await db.SaveChangesAsync();

//    Results.Created($"/todoitems/{todo.Id}", todo);
//});

//// Update an existing item
//todoItems.MapPut("/{id}", async (int id, Todo inTodo, TodoDb db) =>
//{
//    var todo = await db.Todos.FindAsync(id);
//    if (todo is null)
//    {
//        return Results.NotFound();
//    }

//    todo.Name = inTodo.Name;
//    todo.IsComplete = inTodo.IsComplete;

//    await db.SaveChangesAsync();
//    return Results.NoContent();
//});

//// Delete a requested one

//todoItems.MapDelete("/{id}", async (int id, TodoDb db) =>
//{
//    var todo = await db.Todos.FindAsync(id);
//    if (todo is null)
//    {
//        return Results.NotFound();
//    }

//    db.Todos.Remove(todo);
//    await db.SaveChangesAsync();

//    return Results.Ok(todo);
//});

// Changing lamda expression to typedresults

// Changing to project specific routings

// Get all todoitems
app.MapGet("/todoitems", GetTodoItems);

// Get only completed items
app.MapGet("/todoitems/complete", GetCompletedTodoItems);


// Get requested item
app.MapGet("/todoitems/{id}", GetTodoItem);

// Create a new todo item
app.MapPost("/todoitems", InsertNewTodoItem);

// Update an existing item
app.MapPut("/todoitems/{id}", UpdateToDoItem);

// Delete a requested one

app.MapDelete("/todoitems/{id}", DeleteTodoItem);

app.Run();

Func<TodoDb, Task<List<Todo>>> GetTodoItems()
{
    // TypedResults is not supportive in current configured .net framework.
    return async (TodoDb db) =>
              await db.Todos.ToListAsync();
}

Func<TodoDb, Task<List<Todo>>> GetCompletedTodoItems()
{
    return async (TodoDb db) =>
              await db.Todos.Where(x => x.IsComplete).ToListAsync();
}

Func<int, TodoDb, Task<IResult>> GetTodoItem()
{
    return async (int id, TodoDb db) =>
               await db.Todos.FindAsync(id)
               is Todo todo
                ? Results.Ok(todo)
                : Results.NotFound();
}

Func<Todo, TodoDb, Task> InsertNewTodoItem()
{
    return async (Todo todo, TodoDb db) =>
    {
        db.Todos.Add(todo);
        await db.SaveChangesAsync();

        Results.Created($"/todoitems/{todo.Id}", todo);
    };
}

Func<int, Todo, TodoDb, Task<IResult>> UpdateToDoItem()
{
    return async (int id, Todo inTodo, TodoDb db) =>
    {
        var todo = await db.Todos.FindAsync(id);
        if (todo is null)
        {
            return Results.NotFound();
        }

        todo.Name = inTodo.Name;
        todo.IsComplete = inTodo.IsComplete;

        await db.SaveChangesAsync();
        return Results.NoContent();
    };
}

Func<int, TodoDb, Task<IResult>> DeleteTodoItem()
{
    return async (int id, TodoDb db) =>
    {
        var todo = await db.Todos.FindAsync(id);
        if (todo is null)
        {
            return Results.NotFound();
        }

        db.Todos.Remove(todo);
        await db.SaveChangesAsync();

        return Results.Ok(todo);
    };
}