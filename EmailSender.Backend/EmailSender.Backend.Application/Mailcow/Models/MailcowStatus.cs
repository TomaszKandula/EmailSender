using Newtonsoft.Json;

namespace EmailSender.Backend.Application.Mailcow.Models;

public class MailcowStatus
{
    [JsonProperty("ipv6nat-mailcow")]
    public StatusItem? Ipv6Nat { get; set; } 

    [JsonProperty("watchdog-mailcow")]
    public StatusItem? Watchdog { get; set; } 

    [JsonProperty("acme-mailcow")]
    public StatusItem? Acme { get; set; } 

    [JsonProperty("ofelia-mailcow")]
    public StatusItem? Ofelia { get; set; } 

    [JsonProperty("rspamd-mailcow")]
    public StatusItem? Rspamd { get; set; } 

    [JsonProperty("nginx-mailcow")]
    public StatusItem? Nginx { get; set; } 

    [JsonProperty("postfix-mailcow")]
    public StatusItem? Postfix { get; set; } 

    [JsonProperty("dovecot-mailcow")]
    public StatusItem? DoveCot { get; set; } 

    [JsonProperty("php-fpm-mailcow")]
    public StatusItem? PhpFpm { get; set; } 

    [JsonProperty("mysql-mailcow")]
    public StatusItem? MySql { get; set; } 

    [JsonProperty("redis-mailcow")]
    public StatusItem? Redis { get; set; } 

    [JsonProperty("solr-mailcow")]
    public StatusItem? Solr { get; set; } 

    [JsonProperty("clamd-mailcow")]
    public StatusItem? Clamd { get; set; } 

    [JsonProperty("dockerapi-mailcow")]
    public StatusItem? DockerApi { get; set; } 

    [JsonProperty("memcached-mailcow")]
    public StatusItem? MemCached { get; set; } 
    
    [JsonProperty("sogo-mailcow")]
    public StatusItem? SoGo { get; set; } 

    [JsonProperty("unbound-mailcow")]
    public StatusItem? Unbound { get; set; } 

    [JsonProperty("netfilter-mailcow")]
    public StatusItem? NetFilter { get; set; } 

    [JsonProperty("olefy-mailcow")]
    public StatusItem? Olefy { get; set; } 
}