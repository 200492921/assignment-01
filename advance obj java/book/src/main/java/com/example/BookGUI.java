package com.example;

import javafx.application.Application;
import javafx.geometry.Insets;
import javafx.scene.Scene;
import javafx.scene.control.Alert;
import javafx.scene.control.Button;
import javafx.scene.control.Label;
import javafx.scene.control.TextField;
import javafx.scene.layout.GridPane;
import javafx.scene.layout.HBox;
import javafx.scene.layout.VBox;
import javafx.stage.Stage;

public class BookGUI extends Application {

    public static void main(String[] args) {
        launch(args);
    }

    @Override
    public void start(Stage primaryStage) {
        primaryStage.setTitle("Book Information Form");

        // Create components
        TextField titleField = new TextField();
        TextField authorField = new TextField();
        TextField genreField = new TextField();
        TextField yearField = new TextField();
        Button submitButton = new Button("Submit");
        Label resultLabel = new Label();

        // Set up event handling for the submit button
        submitButton.setOnAction(event -> {
            // Get data from text fields
            String title = titleField.getText();
            String author = authorField.getText();
            String genre = genreField.getText();
            try {
                int year = Integer.parseInt(yearField.getText());
                // Instantiate the Book class
                Book book = new Book(title, author, genre, year);

                // Display the result in the label
                resultLabel.setText("Book Details: " + book.toString());
            } catch (NumberFormatException e) {
                showAlert("Error", "Entered value is not an integer. Please enter a valid integer.");
            }
        });

        // Create layout using GridPane for a more structured appearance
        GridPane gridPane = new GridPane();
        gridPane.setHgap(10);
        gridPane.setVgap(10);
        gridPane.setPadding(new Insets(20, 20, 20, 20));

        gridPane.add(new Label("Title:"), 0, 0);
        gridPane.add(titleField, 1, 0);
        gridPane.add(new Label("Author:"), 0, 1);
        gridPane.add(authorField, 1, 1);
        gridPane.add(new Label("Genre:"), 0, 2);
        gridPane.add(genreField, 1, 2);
        gridPane.add(new Label("Year:"), 0, 3);
        gridPane.add(yearField, 1, 3);
        gridPane.add(submitButton, 0, 4, 2, 1);

        // Create a VBox to hold the result label
        VBox vbox = new VBox(10);
        vbox.getChildren().addAll(gridPane, resultLabel);

        // Set up the scene
        Scene scene = new Scene(vbox, 400, 300);
        primaryStage.setScene(scene);

        // Show the stage
        primaryStage.show();
    }

    private void showAlert(String title, String content) {
        Alert alert = new Alert(Alert.AlertType.INFORMATION);
        alert.setTitle(title);
        alert.setHeaderText(null);
        alert.setContentText(content);
        alert.showAndWait();
    }

    // Book class
    static class Book {
        private String title;
        private String author;
        private String genre;
        private int year;

        public Book(String title, String author, String genre, int year) {
            this.title = title;
            this.author = author;
            this.genre = genre;
            this.year = year;
        }

        @Override
        public String toString() {
            return "Title: " + title + "\nAuthor: " + author + "\nGenre: " + genre + "\nYear: " + year;
        }
    }
}
