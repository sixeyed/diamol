import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.util.HashMap; 
import java.util.Map;

public class ConfigLoader {

    public static void main(String[] args) {
        Map<String, String> overrides = new HashMap<>();

        //read file overrides:
        String sourcePath = System.getenv("CONFIG_SOURCE_PATH");
        if (sourcePath.length() > 0) {
            try {
                String content = Files.readString(Paths.get(sourcePath));
                if (content.length() > 0) {
                    String[] values = content.split(System.lineSeparator());
                    for (String value : values) {
                        String[] configParts = value.split("=");
                        overrides.put(configParts[0].toLowerCase(), configParts[1]);
                    }
                }
            } 
            catch (IOException e)  {
                e.printStackTrace();
            }
        }

        // read env var overrides:
        Map<String, String> env = System.getenv();
        for (String envName : env.keySet()) {
            if (envName.toUpperCase().startsWith("IOTD_")){                
                overrides.put(envName.substring(5).replace('_', '.').toLowerCase(), env.get(envName));
            }
        }

        // write out the target file:
        String targetPath = System.getenv("CONFIG_TARGET_PATH");
        StringBuilder configBuilder = new StringBuilder();
        for (String overrideName : overrides.keySet()) {            
            configBuilder.append(overrideName + "=" + overrides.get(overrideName));
            configBuilder.append(System.lineSeparator());
        }
        try {
            Files.writeString(Paths.get(targetPath), configBuilder);
            System.out.println("** Wrote new config file");
        } 
        catch (IOException e) {
            e.printStackTrace();
        }
    }
}