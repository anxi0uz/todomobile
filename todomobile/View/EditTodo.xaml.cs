using todomobile.ViewModels;

namespace todomobile.View;

public partial class EditTodo : ContentPage
{
	public EditTodo(EditTodoViewModel vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}
}