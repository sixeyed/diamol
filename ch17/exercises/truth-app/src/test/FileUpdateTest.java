import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Paths;

public class FileUpdateTest {

    public static void main(String[] args) {
        String path = "truth.txt";
        String contents = "false";
        try {
            Files.writeString(Paths.get(path), contents);
        } 
        catch (IOException e) {
            e.printStackTrace();
        }
    }
}