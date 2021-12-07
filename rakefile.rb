require 'fileutils'
require 'rexml/document'
require 'nuget_helper'
require 'nokogiri'
include FileUtils

task :default => [:all]

desc "Run everything!"
task :all => ["migrations:run"]
$pwd = File.dirname(__FILE__)

# Below: migrations, not part of the regular build
namespace :migrations do
  # install https://www.nuget.org/packages/FluentMigrator.DotNet.Cli
  def sqlite
    ["--connection \"Data Source=#{File.join($pwd,".db.sqlite")};\"", 
     "--processor SQLite", "--assembly DbMigrations.dll"]
  end
  def dotnet_fm
    "dotnet tool run dotnet-fm"
  end

  desc "Run migrations"
  task :run, [:version] do |t,args|
    #to migrate back, you can use "rake migrations:run[1]", where 1 is the desired version
    args.with_defaults(:version => nil)
    params = sqlite
    version = args[:version]
    if version
      params.push(" --version "+version)
    end
    cd File.join("migrations","DbMigrations","bin","Debug","netstandard2.0") do
      system("#{dotnet_fm} migrate #{params.join(" ")}")
    end
  end

  desc "Dry run migrations"
  task :dryrun do |t,args|
    cd File.join("migrations","DbMigrations","bin","Debug","netstandard2.0") do
      params = sqlite
      params.push("--preview")
      system("#{dotnet_fm} migrate #{params.join(" ")}")
    end
  end
end
