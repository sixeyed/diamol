package truth;

import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Paths;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

@RestController
public class TruthController {

    private static final Logger log = LoggerFactory.getLogger(TruthController.class);

    @RequestMapping("/truth")
    public String get() {
        log.debug("** GET /truth called"); 

        String content = "unknown";
        try {
            content = new String(Files.readAllBytes(Paths.get("truth.txt")));
        } 
        catch (IOException e) {
            e.printStackTrace();
        }
        return content;
    }
}
