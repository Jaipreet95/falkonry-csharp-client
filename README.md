[![Falkonry Logo](http://static1.squarespace.com/static/55a7df64e4b09f03368a7a78/t/569c6441ab281050fe32c18a/1453089858079/15-logo-transparent-h.png?format=500w)](http://falkonry.com/)


Falkonry C# Client to access [Falkonry Condition Prediction](falkonry.com) APIs

## Features

    * Create Eventbuffer
    * Create Pipeline
    * Create Subscription
    * Create Publication
    * Retrieve Eventbuffers
    * Retrieve Pipelines
    * Add data to Eventbuffer (json, stream)
    * Retrieve output of a Pipeline
    * Add verification data to Pipeline.
    
## Quick Start

    * To create Eventbuffer
    
```
    using falkonry_csharp_client;
    using falkonry_csharp_client.helper.models;
    
    string token="Add your token here";   
    Falkonry falkonry = new Falkonry("http://localhost:8080", token);
    List<Eventbuffer> eventbuffers = new List<Eventbuffer>();
    string name="event buffer name here";
    Eventbuffer eb = new Eventbuffer();
    eb.name = name;
    SortedDictionary<string, string> options = new SortedDictionary<string, string>();
    options.Add("timeIdentifier", "time");
    options.Add("timeFormat", "iso_8601");
    Eventbuffer eventbufferCreated = falkonry.createEventbuffer(eb, options);
```

    * To create Pipeline
    
```
    using falkonry_csharp_client;
    using falkonry_csharp_client.helper.models;

    string token="Add your token here";   
    Falkonry falkonry = new Falkonry("http://localhost:8080", token);
    
    List<Pipeline> pipelines = new List<Pipeline>();
    List<Eventbuffer> eventbuffers = new List<Eventbuffer>();
    string name = "event buffer name here";
    Eventbuffer eb = new Eventbuffer();
    eb.name = name;
    List<Signal> signals = new List<Signal>();
    Signal signal1 = new Signal();
    signal1.name = "current";
    falkonry_csharp_client.helper.models.ValueType valuetype1 = new falkonry_csharp_client.helper.models.ValueType();
    valuetype1.type = "Numeric";
    EventType eventtype1 = new EventType();
    eventtype1.type = "Samples";
    signal1.eventType = eventtype1;
    signal1.valueType = valuetype1;
    signals.Add(signal1);

    Signal signal2 = new Signal();
    signal2.name = "vibration";
    falkonry_csharp_client.helper.models.ValueType valuetype2 = new falkonry_csharp_client.helper.models.ValueType();
    valuetype2.type = "Numeric";
    EventType eventtype2 = new EventType();
    eventtype2.type = "Samples";
    signal2.eventType = eventtype2;
    signal2.valueType = valuetype2;
    signals.Add(signal2);

    Signal signal3 = new Signal();
    signal3.name = "state";
    falkonry_csharp_client.helper.models.ValueType valuetype3 = new falkonry_csharp_client.helper.models.ValueType();
    valuetype3.type = "Categorical";
    EventType eventtype3 = new EventType();
    eventtype3.type = "Samples";
    signal3.eventType = eventtype3;
    signal3.valueType = valuetype3;
    signals.Add(signal3);

    List<string> inputList = new List<string>();
    inputList.Add("current");
    inputList.Add("vibration");
    inputList.Add("state");

    List<Assessment> assessments = new List<Assessment>();
    Assessment assessment = new Assessment();
    assessment.name = "Health";
    assessment.inputList = inputList;
    assessments.Add(assessment);

    SortedDictionary<string, string> options = new SortedDictionary<string, string>();
    options.Add("timeIdentifier", "time");
    options.Add("timeFormat", "iso_8601");
    options.Add("data", "time, current, vibration, state\n" + "2016-03-01 01:01:01, 12.4, 3.4, On");
    options.Add("fileFormat", "csv");

    Eventbuffer eventbuffer = falkonry.createEventbuffer(eb, options);
    Interval interval = new Interval();
    interval.duration = "PT1S";
    Pipeline pipeline = new Pipeline();
    string pipelineName = "PIPELINE NAME HERE";
    pipeline.name = pipelineName;

    pipeline.inputList = (signals);
    pipeline.singleThingID = (pipelineName);
    pipeline.thingIdentifier = ("thing");
    pipeline.assessmentList = (assessments);
    pipeline.interval = (interval);
    pipeline.input = eventbuffer.id;
    //pipeline.input should be equal to the event buffer ID
    Pipeline createdPipeline = falkonry.createPipeline(pipeline);

    
```

    * To create Subscription
    
```
    using falkonry_csharp_client;
    using falkonry_csharp_client.helper.models;

    string token="Add your token here";   
    Falkonry falkonry = new Falkonry("http://localhost:8080", token);

    //Create Event Buffer First
    
    Subscription sub = new Subscription();
    sub.type = "MQTT";
    sub.path = ("mqtt://test.mosquito.com");
    sub.topic = ("falkonry-eb-1-test");
    sub.username = ("test-user");
    sub.password = ("test");
    sub.timeFormat = ("YYYY-MM-DD HH:mm:ss");
    sub.timeIdentifier = ("time");
    sub.isHistorian = (true);
    sub.valueColumn = ("value");
    sub.signalsDelimiter = ("_");
    sub.signalsTagField = ("tag");
    sub.signalsLocation = ("prefix");

    Subscription subscriptionCreated = falkonry.createSubscription(<EventBuffer ID>, sub);
```

    * To create Publication
    
```
    using falkonry_csharp_client;
    using falkonry_csharp_client.helper.models;

    string token="Add your token here";   
    Falkonry falkonry = new Falkonry("http://localhost:8080", token);

    //Create Event Buffer First
    //Then take the Event Buffer ID and create a Pipeline using it.

    Publication publication = new Publication();
    publication.type = ("MQTT");
    publication.path = ("mqtt://test.mosquito.com");
    publication.topic = ("falkonry-eb-1-test");
    publication.username = ("test-user");
    publication.password = ("test");
    publication.contentType = ("application/json");
    Publication publicationCreated = falkonry.createPublication(<PipelineID>, publication);
```

    * To get all Eventbuffers
    
```
    using falkonry_csharp_client;
    using falkonry_csharp_client.helper.models;

    Falkonry falkonry = new Falkonry("http://localhost:8080", "");
    List<Eventbuffer> eventBuffers=falkonry.getEventbuffers();
```

    * To get all Pipelines
    
```
    using falkonry_csharp_client;
    using falkonry_csharp_client.helper.models;

    Falkonry falkonry = new Falkonry("http://localhost:8080", "");
    List<Pipeline> pipelinelist = falkonry.getPipelines();
```

    * To add data in Eventbuffer
    
```
    using falkonry_csharp_client;
    using falkonry_csharp_client.helper.models;

    string token="Add your token here";   
    Falkonry falkonry = new Falkonry("http://localhost:8080", token);

    //Creating an event bufffer to add data to later
    string name="event buffer name here";
    Eventbuffer eb = new Eventbuffer();
    eb.name = name;
    SortedDictionary<string, string> options = new SortedDictionary<string, string>();

    options.Add("timeIdentifier", "time");
    options.Add("timeFormat", "iso_8601");
    Eventbuffer eventbuffer = falkonry.createEventbuffer(eb, options);
    string data = "{\"time\" :\"2016-03-01 01:01:01\", \"current\" : 12.4, \"vibration\" : 3.4, \"state\" : \"On\"}";
    options.Add("fileFormat", "json");
    InputStatus inputstatus = falkonry.addInput(eventbuffer.id, data, options);
```

    * To add data from a stream in Eventbuffer
    
```
    using falkonry_csharp_client;
    using falkonry_csharp_client.helper.models;

    string token="Add your token here";   
    Falkonry falkonry = new Falkonry("http://localhost:8080", token);

    string name="event buffer name here";
    Eventbuffer eb = new Eventbuffer();
    eb.name = name;
    SortedDictionary<string, string> options = new SortedDictionary<string, string>();
    options.Add("timeIdentifier", "time");
    options.Add("timeFormat", "iso_8601");
    Eventbuffer eventbuffer = falkonry.createEventbuffer(eb, options);
    /*This particular example will read data from a AddData.json file in debug folder in bin*/
    string folder_path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

    string path = folder_path + "/AddData.json";
    //Alternatively, you can directly specify the folder path in the "folder_path" variable

    byte[] bytes = System.IO.File.ReadAllBytes(path);

    InputStatus inputstatus = falkonry.addInputStream(eventbuffer.id, bytes, options);
    //The updated event buffer
    eventbuffer = falkonry.getEventBuffer(eventbuffer.id);
```

    * To get output of a Pipeline
    
```
    using falkonry_csharp_client;
    using falkonry_csharp_client.helper.models;

    string token="Add your token here";   
    Falkonry falkonry = new Falkonry("http://localhost:8080", token);

    string pipeline_id ="pipeline ID here";
    System.IO.Stream streamrecieved = falkonry.getOutput(pipeline_id, null, null);
    //The folder path below by default is debug in bin.
    string folder_path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
    //Alternatively, you can also specify the path of the folder in thr folder_path variable
    //The outflow will get saved in an outflow.txt file there
    string path = folder_path + "/outflow.txt";

    System.IO.StreamReader streamreader = new System.IO.StreamReader(streamrecieved);
    System.IO.StreamWriter streamwriter = new System.IO.StreamWriter(path);
    string line;
    using (streamwriter)
    {
        while ((line = streamreader.ReadLine()) != null)
        {
            streamwriter.WriteLine(line);
        }
    }
```
    * To add verification data
    
```
    using falkonry_csharp_client;
    using falkonry_csharp_client.helper.models;

    string token = "Add your token here";   
    options = null;
    Falkonry falkonry = new Falkonry("http://localhost:8080", token);
    string data = "time,end,car,Health\n2011-03-31T00:00:00Z,2011-04-01T00:00:00Z,IL9753,Normal\n2011-03-31T00:00:00Z,2011-04-01T00:00:00Z,HI3821,Normal";
    string response = falkonry.addVerification(<pipelineID>,data, options);

```
   * To add verification data from stream
    
```
    using falkonry_csharp_client;
    using falkonry_csharp_client.helper.models;

    string token="Add your token here";   
    Falkonry falkonry = new Falkonry("http://localhost:8080", token);
    string path = "Insert the path to your file here";
    byte[] bytes = System.IO.File.ReadAllBytes(path);
    string response = falkonry.addVerificationStream(<pipelineID>, bytes, null);

```     


## Docs

    * [Falkonry APIs](https://service.falkonry.io/api)
     

## License

  Available under [MIT License](LICENSE)