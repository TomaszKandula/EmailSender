using System.Text.Json.Serialization;

namespace EmailSender.Backend.Application.Mailcow.Models;

public class MailcowStatus
{
    [JsonPropertyName("ipv6nat-mailcow")]
    public StatusItem? Ipv6Nat { get; set; } 

    [JsonPropertyName("watchdog-mailcow")]
    public StatusItem? Watchdog { get; set; } 

    [JsonPropertyName("acme-mailcow")]
    public StatusItem? Acme { get; set; } 

    [JsonPropertyName("ofelia-mailcow")]
    public StatusItem? Ofelia { get; set; } 

    [JsonPropertyName("rspamd-mailcow")]
    public StatusItem? Rspamd { get; set; } 

    [JsonPropertyName("nginx-mailcow")]
    public StatusItem? Nginx { get; set; } 

    [JsonPropertyName("postfix-mailcow")]
    public StatusItem? Postfix { get; set; } 

    [JsonPropertyName("dovecot-mailcow")]
    public StatusItem? DoveCot { get; set; } 

    [JsonPropertyName("php-fpm-mailcow")]
    public StatusItem? PhpFpm { get; set; } 

    [JsonPropertyName("mysql-mailcow")]
    public StatusItem? MySql { get; set; } 

    [JsonPropertyName("redis-mailcow")]
    public StatusItem? Redis { get; set; } 

    [JsonPropertyName("solr-mailcow")]
    public StatusItem? Solr { get; set; } 

    [JsonPropertyName("clamd-mailcow")]
    public StatusItem? Clamd { get; set; } 

    [JsonPropertyName("dockerapi-mailcow")]
    public StatusItem? DockerApi { get; set; } 

    [JsonPropertyName("memcached-mailcow")]
    public StatusItem? MemCached { get; set; } 
    
    [JsonPropertyName("sogo-mailcow")]
    public StatusItem? SoGo { get; set; } 

    [JsonPropertyName("unbound-mailcow")]
    public StatusItem? Unbound { get; set; } 

    [JsonPropertyName("netfilter-mailcow")]
    public StatusItem? NetFilter { get; set; } 

    [JsonPropertyName("olefy-mailcow")]
    public StatusItem? Olefy { get; set; } 
}