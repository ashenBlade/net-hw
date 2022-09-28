import {Message, parseMessage} from "../models/message";
import {AMQPChannel, AMQPConsumer, AMQPQueue, AMQPWebSocketClient} from "@cloudamqp/amqp-client";
import {AMQPBaseClient} from "@cloudamqp/amqp-client/types/amqp-base-client";
import {ForumCommunicator} from "./forumCommunicator";
import {MessageCallback} from "./messageCallback";
import {RabbitMqMessagePublishedEvent} from "../models/rabbitMqMessagePublishedEvent";

export class RabbitmqForumCommunicator implements ForumCommunicator {
    amqp?: AMQPWebSocketClient
    client?: AMQPBaseClient
    channel?: AMQPChannel
    queue?: AMQPQueue
    consumer?: AMQPConsumer
    callbacks: MessageCallback[]

    constructor(readonly url: string,
                readonly vhost: string = '/',
                readonly username: string = 'guest',
                readonly password: string = 'guest',
                readonly queueName: string = '',
                readonly exchange: string = 'amq.fanout',
                readonly routingKey: string = '') {
        this.callbacks = [];
    }

    async open() {
        this.amqp = new AMQPWebSocketClient(this.url, this.vhost, this.username, this.password);
        this.client = await this.amqp.connect();
        this.channel = await this.client.channel();
        this.queue = await this.channel.queue(this.queueName);
        await this.queue.bind(this.exchange, this.routingKey);
        this.consumer = await this.queue.subscribe({}, (msg) => {
            const body = msg.bodyToString();
            if (body === null) {
                console.error('Received body is null');
                return;
            }
            let message: Message;
            try {
                message = RabbitMqMessagePublishedEvent.fromString(body).toMessage();
            } catch (e) {
                console.error('Could not parse received message', e);
                return;
            }


            for (const callback of this.callbacks) {
                try {
                    callback(message);
                } catch (e) {
                    console.error('Error occurred in callback');
                }
            }
        });
    }

    private async resetSubscribers() {
        this.callbacks.length = 0;
    }

    unregisterOnMessageCallback(cb: MessageCallback): void {
        const index = this.callbacks.indexOf(cb);
        if (index !== -1) {
            this.callbacks.splice(index, 1);
        }
    }

    async close() {
        await this.resetSubscribers();
        if (this.amqp && !this.amqp.closed) {
            await this.amqp.close()
        }
    }

    registerOnMessageCallback(cb: MessageCallback): void {
        this.callbacks.push(cb);
    }

    async sendMessage(msg: Message): Promise<void> {
        if (!this.channel) {
            throw new Error('Channel is not opened')
        }
        const event = new RabbitMqMessagePublishedEvent(msg.username, msg.message);
        await this.channel.basicPublish(this.exchange, this.routingKey, JSON.stringify(event));
    }

    getMessagesFromEnd(page: number, size: number): Promise<Message[]> {
        return Promise.resolve([]);
    }
}