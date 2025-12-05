using todomobile.ViewModels;

namespace todomobile.View;

public partial class AddTodo : ContentPage
{
	public AddTodo(AddTodoViewModel viewModel)
	{
		this.BindingContext = viewModel;
		InitializeComponent();
	}
}