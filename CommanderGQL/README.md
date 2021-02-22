docker-compose stop
docker-compose up -d

dotnet build
dotnet tool install --global dotnet-ef
dotnet tool update --global dotnet-ef

dotnet ef migrations add AddPlatformToDB
dotnet ef database update

dotnet run

----- INSOMNIA

get commands/platforms 

query{
  command{
    id
    howTo
    platform{
      name
    }
  }
}

query{
  platform{
    id
    name
  }
}

query{
  platform{
    id
    name
    commands{
      id
      howTo
      commandLine
    }
  }
}

---

sorting query

query{
  platform(order:{name:ASC}){
    name
  }
}

----

filter query 

query{
  command(where : {platformId: {eq: 1}})
  {
    id
    howTo
    commandLine
    platform{
     name
    }
  }
}

-----

parallel platforms 

query{
  a:platform{
    id
    name
  }
    b:platform{
    id
    name
  }
    c:platform{
    id
    name
  }
}


-----------
MUTATION

add platform

mutation{
  addPlatform(input: {
    name: "REDHAT"
  })
  {
    platform
    {
    id
    name
    }
  }
} 

addCommand 

mutation{
  addCommand(input: {
    howTo: "Perform directory listing"
    platformId: 5
    commandLine:"ls"
  })
  {
    command{
      id
    }
  }
}
