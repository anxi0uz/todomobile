using todomobile.ViewModels;

namespace todomobile.View;

public partial class TodoPage : ContentPage
{
	public TodoPage(TodoViewModel viewModel)
	{
		this.BindingContext = viewModel;
		InitializeComponent();
	}
}